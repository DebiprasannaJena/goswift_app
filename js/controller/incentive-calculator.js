angular.module('incentiveCalculator', [])
	.filter('percentage', ['$filter', function ($filter) {
		return function (input, decimals) {
			return $filter('number')(input * 100, decimals) + '%';
		};
	}])
	.directive('bindHtmlUnsafe', function ($compile) {
		return function ($scope, $element, $attrs) {

			var compile = function (newHTML) { // Create re-useable compile function
				newHTML = $compile(newHTML)($scope); // Compile html
				$element.html('').append(newHTML); // Clear and append it
			};

			var htmlName = $attrs.bindHtmlUnsafe; // Get the name of the variable 
			// Where the HTML is stored

			$scope.$watch(htmlName, function (newHTML) { // Watch for changes to 
				// the HTML
				if (!newHTML) return;
				compile(newHTML);   // Compile it
			});

		};
	})
	.directive('numbersOnly', function () {
		return {
			require: 'ngModel',
			link: function (scope, element, attr, ngModelCtrl) {
				function fromUser(text) {
					if (text) {
						var transformedInput = text.replace(/[^0-9.{1,2}]/g, '');

						if (transformedInput !== text) {
							ngModelCtrl.$setViewValue(transformedInput);
							ngModelCtrl.$render();
						}
						return transformedInput;
					}
					return undefined;
				}            
				ngModelCtrl.$parsers.push(fromUser);
			}
		};
	})
	.directive('onFinishRender', function ($timeout) {
		return {
			restrict: 'A',
			link: function (scope, element, attr) {
				if (scope.$last === true) {
					$timeout(function () {
						scope.$emit(attr.onFinishRender);
					});
				}
			}
		}
	})
	.controller('incentiveCalculatorController', function ($scope, $rootScope, $location, $timeout, $window, $http) {

		$scope.flowTitle = 'Incentive Calculator';
		$scope.domainName = 'http://192.168.201.118/swp/';
		// $scope.domainName = 'http://203.129.207.117:81/IPICOL_New/';
		$scope.subsidiesForm = true;
		$scope.subsidiesEligible = false;
		$scope.subsidiesResult = false;
		$scope.master = {};
		$scope.user = { gender: "male" };
		$scope.refNum = Math.floor(100000 + Math.random() * 900000);
		$scope.capitalSubsidyVal = '';
		$scope.interestSubsidyVal = '';
		$scope.capitalSubsidyFinalVal = '';
		$scope.interestSubsidyFinalVal = '';
		$scope.kbkDistrict = false;

		$http.get($scope.domainName + 'js/controller/incentive-calculator-data.json')
			.success(function (data) {
				$scope.incentiveDataObj = data;
			})
			.error(function () {
				console.log('Something went wrong in file calling.');
			});
		/***************** START HERE: select option values ***************/

		$scope.sectorDropdown = [
			{ text: '--Select Sector--', value: '0' },
			{ text: 'Ancillary and Downstream', value: 'ancillaryandDownstream' },
			{ text: 'Petroleum, Chemical and Petrochemicals', value: 'Petrochemicals' },
			{ text: 'IT/ITES', value: 'IT/ITES' },
			{ text: 'ESDM', value: 'ESDM' },
			{ text: 'Agro/Food Processing including Seafood', value: 'Agro/Food' },
			{ text: 'Textiles (including Technical Textile & Apparel)', value: 'Textiles' },
			{ text: 'Bio-technology', value: 'Biotechnology' },
			{ text: 'Pharmaceuticals', value: 'Pharmaceuticals' },
			{ text: 'Tourism and Hospitality', value: 'Tourism' },
			{ text: 'Healthcare', value: 'Healthcare' },
			{ text: 'Renewable Energy Projects', value: 'RenewableProjects' },
			{ text: 'Renewable Energy Manufacturing', value: 'RenewableManufacturing' },
			{ text: 'Automobiles and Auto-components', value: 'Automobiles' },
			{ text: 'Manufacturing in Aviation and Maintenance Repair &Overhaul (MRO) facilities', value: 'ManufacturingAviation' },
			{ text: 'Fly ash & Blast furnace slag based industries utilizing a minimum of 25% by weight as base raw material', value: 'Flyash' },
			{ text: 'Gem stone cutting and polishing', value: 'Gemstone' },
			{ text: 'Handicraft, Handloom, Coir and Leather products', value: 'Handicraft' },
			{ text: 'Plastics and Polymers', value: 'PlasticsandPolymers' },
			{ text: 'Shipbuilding and construction of other floating vessels/ Ship repair', value: 'Shipbuilding' }
		];

		$scope.distobj = [
			{ text: '--Select District--', value: '0' },
			{ text: 'BARGARH', value: 'A' },
			{ text: 'JHARSUGUDA', value: 'A' },
			{ text: 'SUNDARGARH', value: 'A' },
			{ text: 'SAMBALPUR', value: 'A' },
			{ text: 'DEBAGARH', value: 'A' },
			{ text: 'KENDUJHAR', value: 'A' },
			{ text: 'BALESHWAR', value: 'A' },
			{ text: 'BHADRAK', value: 'A' },
			{ text: 'JAJAPUR', value: 'A' },
			{ text: 'DHENKANAL', value: 'A' },
			{ text: 'ANUGUL', value: 'A' },
			{ text: 'BAUDH', value: 'A' },
			{ text: 'CUTTACK', value: 'A' },
			{ text: 'KENDRAPARA', value: 'A' },
			{ text: 'JAGATSINGHPUR', value: 'A' },
			{ text: 'PURI', value: 'A' },
			{ text: 'KHORDHA', value: 'A' },
			{ text: 'NAYAGARH', value: 'A' },
			{ text: 'GANJAM', value: 'A' },
			{ text: 'MAYURBHANJ', value: 'B' },
			{ text: 'SUBARNAPUR', value: 'B' },
			{ text: 'BOLANGIR', value: 'B' },
			{ text: 'NUAPADA', value: 'B' },
			{ text: 'NAWRANGPUR', value: 'B' },
			{ text: 'KALAHANDI', value: 'B' },
			{ text: 'KANDHAMAL', value: 'B' },
			{ text: 'GAJAPATI', value: 'B' },
			{ text: 'RAYAGADA', value: 'B' },
			{ text: 'KORAPUT', value: 'B' },
			{ text: 'MALKANGIRI', value: 'B' }
		];
		$scope.enterpriseTypes = [
			{ text: '--Select Type--', value: '0' }
		];
		$scope.employmentRangeVal = [
			{ text: 'Less than 20', value: '<20' },
			{ text: '20-40', value: '20-40' },
			{ text: '41-75', value: '41-75' },
			{ text: '76-80', value: '76-80' },
			{ text: '81-100', value: '81-100' },
			{ text: '101-150', value: '101-150' },
			{ text: '151-180', value: '151-180' },
			{ text: '181-200', value: '181-200' },
			{ text: '201-250', value: '201-250' },
			{ text: '251-300', value: '251-300' },
			{ text: '301-500', value: '301-500' },
			{ text: '501-750', value: '501-750' },
			{ text: '751-1000', value: '751-1000' },
			{ text: '1001-1500', value: '1001-1500' },
			{ text: 'More than 1500', value: '>1500' }
		];

		/***************** END HERE: select option values *****************/

		/*********  START HERE: select sector chagne function ********/

		$scope.microEnterprises = 0.25;
		$scope.smallEnterprises = 5;
		$scope.mediumEnterprises = 10;
		$scope.infoSectorChange = function (val) {
			$scope.microEnterprises = 0.25;
			$scope.smallEnterprises = 5;
			$scope.mediumEnterprises = 10;

			$scope.sectorVal = val;
			if (val == 'ancillaryandDownstream') {
				$scope.enterpriseTypes = [
					{ text: 'Ancillary and Downstream', value: 'Ancillary and Downstream' }
				]
			}
			else if (val == 'Petrochemicals') {
				$scope.enterpriseTypes = [
					{ text: 'Petroleum, Chemical and Petrochemicals', value: 'Petroleum, Chemical and Petrochemicals' }
				]
			}
			else if (val == 'IT/ITES') {
				$scope.microEnterprises = 0.10;
				$scope.smallEnterprises = 2;
				$scope.mediumEnterprises = 5;
				$scope.enterpriseTypes = [
					{ text: 'IT/ITES', value: 'IT/ITES' }
				]
			}
			else if (val == 'ESDM') {
				$scope.enterpriseTypes = [
					{ text: 'ESDM', value: 'ESDM' }
				]
			}
			else if (val == 'Agro/Food') {
				$scope.enterpriseTypes = [
					{ text: '--Select Type--', value: '0' },
					{ text: 'New Food Processing unit', value: 'New Food Processing unit' }
				]
			}
			else if (val == 'Biotechnology') {
				$scope.enterpriseTypes = [
					{ text: '--Select Type--', value: '0' },
					{ text: 'Entity', value: 'Entity' }
				]
			}
			else if (val == 'Healthcare') {
				$scope.enterpriseTypes = [
					{ text: '--Select Type--', value: '0' },
					{ text: 'New Grade 1 hospitals', value: 'newGrade1' },
					{ text: 'Existing hospitals being upgraded to multi-speciality or super-speciality hospitals with 50% additional bed', value: 'existingHospitalsUpgradedToMulti-speciality' },
					{ text: 'New hospitals in priority districts', value: 'newHospitalInprioityDistricts' }
				]
			}
			else if (val == 'RenewableProjects') {
				$scope.enterpriseTypes = [
					{ text: '--Select Type--', value: '0' },
					{ text: 'Solar Park Developer', value: 'solarParkDeveloper' }
				]
			}
			else if (val == 'RenewableManufacturing') {
				$scope.enterpriseTypes = [
					{ text: 'Renewable Energy Manufacturing', value: 'Renewable Energy Manufacturing' }
				]
			}
			else if (val == 'Textiles') {
				$scope.enterpriseTypes = [
					{ text: '--Select Type--', value: '0' },
					{ text: 'Special Purpose Vehicle', value: 'Special Purpose Vehicle' },
					{ text: 'New Apparel units with at least 200 workers & 90% dmicile of', value: 'New Apparel units with at least 200 workers & 90% dmicile of' }
				]
			}
			else if (val == 'Pharmaceuticals') {
				$scope.enterpriseTypes = [
					{ text: '--Select Type--', value: '0' },
					{ text: 'Pharmaceutical units under A0 & B0 category', value: 'Pharmaceutical units under A0 & B0 category' }
				]
			}
			else if (val == 'Tourism') {
				$scope.microEnterprises = 0.10;
				$scope.smallEnterprises = 2;
				$scope.mediumEnterprises = 5;
				$scope.enterpriseTypes = [
					{ text: '--Select Type--', value: '0' },
					{ text: 'Tented Accommodation', value: 'Tented Accommodation' },
					{ text: 'Equipment worth Rs.1 crore & above for Adventure & Water Sport', value: 'Equipment worth Rs.1 crore & above for Adventure & Water Sport' }
				]
			}
			else if (val == 'Automobiles') {
				$scope.enterpriseTypes = [
					{ text: 'Automobiles and Auto-components', value: 'Automobiles and Auto-components' }
				]
			}
			else if (val == 'ManufacturingAviation') {
				$scope.enterpriseTypes = [
					{ text: 'Manufacturing in Aviation and Maintenance Repair &Overhaul (MRO) facilities', value: 'Manufacturing in Aviation and Maintenance Repair &Overhaul (MRO) facilities' }
				]
			}
			else if (val == 'Flyash') {
				$scope.enterpriseTypes = [
					{ text: 'Fly ash & Blast furnace slag based industries utilizing a minimum of 25% by weight as base raw material', value: 'Fly ash & Blast furnace slag based industries utilizing a minimum of 25% by weight as base raw material' }
				]
			}
			else if (val == 'Gemstone') {
				$scope.enterpriseTypes = [
					{ text: 'Gem stone cutting and polishing', value: 'Gem stone cutting and polishing' }
				]
			}
			else if (val == 'Handicraft') {
				$scope.enterpriseTypes = [
					{ text: 'Handicraft, Handloom, Coir and Leather products', value: 'Handicraft, Handloom, Coir and Leather products' }
				]
			}
			else if (val == 'PlasticsandPolymers') {
				$scope.enterpriseTypes = [
					{ text: 'Plastics and Polymers', value: 'Plastics and Polymers' }
				]
			}
			else if (val == 'Shipbuilding') {
				$scope.enterpriseTypes = [
					{ text: 'Shipbuilding and construction of other floating vessels/ Ship repair', value: 'Shipbuilding and construction of other floating vessels/ Ship repair' }
				]
			}
			else {
				$scope.enterpriseTypes = [
					{ text: '--Select Type--', value: '0' }
				]
			}
		}

		/*********  END HERE: select sector chagne function ********/

		/*********  START HERE: District dropdown chagne function ********/

		$scope.districtChange = function (val) {

			$scope.districtVal = val.value;
			$scope.districtName = val.text;
			if ($scope.districtName == 'KALAHANDI' || $scope.districtName == 'BOLANGIR' || $scope.districtName == 'KORAPUT') {
				$scope.kbkDistrict = true;
			}
			else {
				$scope.kbkDistrict = false;
			}
		}

		/*********  END HERE: District dropdown chagne function ********/

		/*********  START HERE: Investor Category dropdown chagne function ********/

		$scope.investorCategoryChange = function (val) {

			$scope.investorCategoryVal = val;

		}

		/*********  END HERE: Investor Category dropdown chagne function ********/

		/*********  START HERE: Enterprise dropdown chagne function ********/

		$scope.enterpriseTypeChange = function (val) {

			$scope.enterpriseVal = val;

		}

		/*********  END HERE: Enterprise dropdown chagne function ********/

		/*********  START HERE: Enterprise dropdown chagne function ********/
		$scope.expectedEmployee = $scope.employmentRangeVal[0].value;
		$scope.employmentRangeChange = function (val) {

			$scope.expectedEmployee = val;

		}

		/*********  END HERE: Enterprise dropdown chagne function ********/

		$scope.totalInvestVal = function(totalInv,plantInv) {
			$scope.totAmnt = Number(totalInv);
			$scope.invAmnt = Number(plantInv);
			if ($scope.totAmnt == '' || isNaN($scope.totAmnt)) {
				alert('Enter Apprx. investment amount.');
				$scope.user.plantInvest = '';
			}
			else if ($scope.invAmnt > $scope.totAmnt) {
				alert('Investment in Plant & Machinery amount can not be greater then Apprx. investment amount.')
				$scope.user.plantInvest = '';
			}
		}
		$scope.plantInvestVal = function (totalInv,plantInv) {
			$scope.totAmnt = Number(totalInv);
			$scope.invAmnt = Number(plantInv);
			if ($scope.totAmnt == '' || isNaN($scope.totAmnt)) {
				alert('Enter Apprx. investment amount.');
				$scope.user.plantInvest = '';
			}
			else if ($scope.invAmnt > $scope.totAmnt) {
				alert('Investment in Plant & Machinery amount can not be greater then Apprx. investment amount.')
				$scope.user.plantInvest = '';
			}
		}

		/*********  START HERE: Incentive form button click function ********/
		$scope.calculateIncentive = function (user) {
			$scope.matchedObj = [];
			$scope.matchedincentiveNameObj = [];
			$scope.finalObj = [];
			$scope.generatedInc = [];
			angular.copy(user, $scope.master);
			$scope.callFun = function (fun, index, item) {
				var retFun = eval("$scope." + fun + "(" + index + ",item)")[0];
				return retFun;
			}
			$scope.getVal = function (fun, index, item) {
				var retVal = eval("$scope." + fun + "(" + index + ",item)")[1];
				return retVal;
			}
			$scope.matchedIncentives = function (policyNm) {
				for (i in policyNm) {
					for (j in $scope.incentiveDataObj) {
						if ($scope.incentiveDataObj[j].policyName == policyNm[i]) {
							$scope.matchedObj.push($scope.incentiveDataObj[j]);
						}
					}
				}
				for (k in $scope.matchedObj) {
					if ($scope.matchedincentiveNameObj.indexOf($scope.matchedObj[k].incentiveName) == -1) {
						$scope.matchedincentiveNameObj.push($scope.matchedObj[k].incentiveName);
					}
				}
				for (x in $scope.matchedincentiveNameObj) {
					$scope.generatedInc = [];
					for (z in $scope.matchedObj) {
						if ($scope.matchedincentiveNameObj[x] == $scope.matchedObj[z].incentiveName) {
							if ($scope.matchedObj[z].policyName == "MSME Incentives in Backward Districts under IPR 2015" ||
								$scope.matchedObj[z].policyName == "MSME Incentives under IPR 2015" ||
								$scope.matchedObj[z].policyName == "MSME Priority Sector Incentives for Ancillary and Downstream in Backward Districts under IPR 2015" ||
								$scope.matchedObj[z].policyName == "MSME Priority Sector Incentives for Ancillary and Downstream under IPR 2015" ||
								$scope.matchedObj[z].policyName == "MSME Priority Sector Incentives in Backward Districts under IPR 2015" ||
								$scope.matchedObj[z].policyName == "MSME Priority Sector Incentives under IPR 2015" ||
								$scope.matchedObj[z].policyName == "Priority Sector Incentives for Ancillary and Downstream in Backward Districts under IPR 2015" ||
								$scope.matchedObj[z].policyName == "Priority Sector Incentives for Ancillary and Downstream under IPR 2015" ||
								$scope.matchedObj[z].policyName == "Priority Sector Incentives in Backward Districts under IPR 2015" ||
								$scope.matchedObj[z].policyName == "Priority Sector Incentives under IPR 2015") {
								$scope.customPolicyNm = "Under IPR 2015";
							}
							else {
								$scope.customPolicyNm = $scope.matchedObj[z].policyName;
							}
							$scope.generatedInc.push({ "policyName": $scope.customPolicyNm, "functionName": $scope.matchedObj[z].functionName, "id": $scope.matchedObj[z].id });
						}
					}
					$scope.finalObj.push({ 'key': $scope.matchedincentiveNameObj[x], 'value': $scope.generatedInc });
					// console.log($scope.finalObj);

					// if(data.hasOwnProperty(key)) {
					// 	var value = data[key];
					// 	//do something with value;
					// }
				}

			}
			if ($scope.sectorVal == "ancillaryandDownstream") {
				if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives for Ancillary and Downstream under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives for Ancillary and Downstream under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives for Ancillary and Downstream in Backward Districts under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives for Ancillary and Downstream in Backward Districts under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 10 && $scope.user.plantInvest <= 100) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives for Ancillary and Downstream under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 10 && $scope.user.plantInvest < 50) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives for Ancillary and Downstream in Backward Districts under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 0 && $scope.user.plantInvest <= 10) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives for Ancillary and Downstream under IPR 2015', 'Under MSME Development policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 0 && $scope.user.plantInvest <= 10) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives for Ancillary and Downstream in Backward Districts under IPR 2015', 'Under MSME Development policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "Agro/Food") {
				if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Food Processing Policy, 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Food Processing Policy, 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Food Processing Policy, 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Food Processing Policy, 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest <= 100) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Food Processing Policy, 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest <= 50) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Food Processing Policy, 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 0 && $scope.user.plantInvest < 10) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016', 'Under Food Processing Policy, 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 0 && $scope.user.plantInvest < 10) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016', 'Under Food Processing Policy, 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "Biotechnology") {
				if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Biotechnology policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Biotechnology policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Biotechnology policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Biotechnology policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest <= 100) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Biotechnology policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest <= 50) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Biotechnology policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 0 && $scope.user.plantInvest < 10) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016', 'Under Biotechnology policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 0 && $scope.user.plantInvest < 10) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016', 'Under Biotechnology policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "Healthcare") {
				if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75') && ($scope.districtName == 'BOLANGIR' || $scope.districtName == 'BAUDH' || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KALAHANDI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'KORAPUT' || $scope.districtName == 'MALKANGIRI' || $scope.districtName == 'NAWRANGPUR' || $scope.districtName == 'NUAPADA' || $scope.districtName == 'RAYAGADA')) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Healthcare policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75') && ($scope.districtName == 'BOLANGIR' || $scope.districtName == 'BAUDH' || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KALAHANDI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'KORAPUT' || $scope.districtName == 'MALKANGIRI' || $scope.districtName == 'NAWRANGPUR' || $scope.districtName == 'NUAPADA' || $scope.districtName == 'RAYAGADA')) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Healthcare policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 5 && $scope.user.plantInvest < 50) && ($scope.districtName == 'BOLANGIR' || $scope.districtName == 'BAUDH' || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KALAHANDI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'KORAPUT' || $scope.districtName == 'MALKANGIRI' || $scope.districtName == 'NAWRANGPUR' || $scope.districtName == 'NUAPADA' || $scope.districtName == 'RAYAGADA')) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Healthcare policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 5) && ($scope.districtName == 'BOLANGIR' || $scope.districtName == 'BAUDH' || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KALAHANDI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'KORAPUT' || $scope.districtName == 'MALKANGIRI' || $scope.districtName == 'NAWRANGPUR' || $scope.districtName == 'NUAPADA' || $scope.districtName == 'RAYAGADA')) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016', 'Under Healthcare policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 5) && ($scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Incentives under IPR 2015', 'Under MSME Development policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "Pharmaceuticals") {
				if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.pharmaCapitalSubsidy = false;
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.pharmaCapitalSubsidy = true;
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 20 && $scope.user.plantInvest < 100) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.pharmaCapitalSubsidy = true;
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 20 && $scope.user.plantInvest < 100) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.pharmaCapitalSubsidy = false;
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 50) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75') && ($scope.districtVal == "B")) {
					$scope.pharmaCapitalSubsidy = false;
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 50) && ($scope.expectedEmployee == '41-75') && ($scope.districtVal == "B")) {
					$scope.pharmaCapitalSubsidy = true;
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 10 && $scope.user.plantInvest < 50) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40') && ($scope.districtVal == "B")) {
					$scope.pharmaCapitalSubsidy = true;
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 10 && $scope.user.plantInvest < 50) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40') && ($scope.districtVal == "B")) {
					$scope.pharmaCapitalSubsidy = false;
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 10 && $scope.user.plantInvest < 20) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.pharmaCapitalSubsidy = false;
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.pharmaCapitalSubsidy = false;
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.districtVal == "B")) {
					$scope.pharmaCapitalSubsidy = false;
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016', 'Under Pharmaceutical policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "IT/ITES" || $scope.sectorVal == "ESDM") {
				if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					if($scope.sectorVal == "ESDM"){
						$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under ICT Policy 2014', 'Special Incentive Package Scheme for ESDM Sector'];
					}
					else {
						$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under ICT Policy 2014'];
					}
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under ICT Policy 2014'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75') && ($scope.districtVal == "B")) {
					if($scope.sectorVal == "ESDM"){
						$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under ICT Policy 2014', 'Special Incentive Package Scheme for ESDM Sector'];
					}
					else {
						$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under ICT Policy 2014'];
					}
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under ICT Policy 2014'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if ((($scope.user.plantInvest > 10 && $scope.user.plantInvest < 100 && $scope.sectorVal == "ESDM") || ($scope.user.plantInvest > 5 && $scope.sectorVal == "IT/ITES")) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under ICT Policy 2014'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if ((($scope.user.plantInvest > 10 && $scope.user.plantInvest < 50 && $scope.sectorVal == "ESDM") || ($scope.user.plantInvest > 5 && $scope.sectorVal == "IT/ITES")) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under ICT Policy 2014'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if ((($scope.user.plantInvest > 0 && $scope.user.plantInvest < 10 && $scope.sectorVal == "ESDM") || ($scope.user.plantInvest > 0 && $scope.user.plantInvest < 5 && $scope.sectorVal == "IT/ITES")) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016', 'Under ICT Policy 2014'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if ((($scope.user.plantInvest > 0 && $scope.user.plantInvest < 10 && $scope.sectorVal == "ESDM") || ($scope.user.plantInvest > 0 && $scope.user.plantInvest < 5 && $scope.sectorVal == "IT/ITES")) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016', 'Under ICT Policy 2014'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "RenewableManufacturing") {
				if (($scope.user.plantInvest >= 11) && ($scope.expectedEmployee != '<20') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Renewable Energy policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11) && ($scope.expectedEmployee == '<20') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Renewable Energy policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11) && ($scope.expectedEmployee != '<20') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Renewable Energy policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11) && ($scope.expectedEmployee == '<20') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Renewable Energy policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016', 'Under Renewable Energy policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016', 'Under Renewable Energy policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "RenewableProjects") {
				if ($scope.user.plantInvest > 10) {
					$scope.policyObj = ['Under Renewable Energy policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if ($scope.user.plantInvest < 10) {
					$scope.policyObj = ['Under MSME Development policy 2016', 'Under Renewable Energy policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "Textiles") {
				if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100' && $scope.expectedEmployee != '101-150' && $scope.expectedEmployee != '151-180') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '101-150' || $scope.expectedEmployee == '151-180') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51 && $scope.user.plantInvest < 100) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100' && $scope.expectedEmployee != '101-150' && $scope.expectedEmployee != '151-180') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51 && $scope.user.plantInvest < 100) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100' || $scope.expectedEmployee == '101-150' || $scope.expectedEmployee == '151-180') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest < 50) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100' && $scope.expectedEmployee != '101-150' && $scope.expectedEmployee != '151-180')) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100' && $scope.expectedEmployee != '101-150' && $scope.expectedEmployee != '151-180') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if ($scope.user.plantInvest >= 51 && ($scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100' || $scope.expectedEmployee == '101-150' || $scope.expectedEmployee == '151-180') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if ($scope.user.plantInvest >= 51 && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest < 50) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100' && $scope.expectedEmployee != '101-150' && $scope.expectedEmployee != '151-180') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest < 50) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100' || $scope.expectedEmployee == '101-150' || $scope.expectedEmployee == '151-180') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest < 50) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100' && $scope.expectedEmployee != '101-150' && $scope.expectedEmployee != '151-180') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 11 && $scope.user.plantInvest < 50) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100' || $scope.expectedEmployee == '101-150' || $scope.expectedEmployee == '151-180') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100' && $scope.expectedEmployee != '101-150' && $scope.expectedEmployee != '151-180') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100' || $scope.expectedEmployee == '101-150' || $scope.expectedEmployee == '151-180') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100' && $scope.expectedEmployee != '101-150' && $scope.expectedEmployee != '151-180') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016', 'Under Apparel policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100' || $scope.expectedEmployee == '101-150' || $scope.expectedEmployee == '151-180') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else if ($scope.sectorVal == "Tourism") {
				if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Tourism Policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Tourism Policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Tourism Policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Tourism Policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 5 && $scope.user.plantInvest < 100) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015', 'Under Tourism Policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 5 && $scope.user.plantInvest < 50) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015', 'Under Tourism Policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 5) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016', 'Under Tourism Policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 5) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016', 'Under Tourism Policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}
			else {
				if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75' && $scope.expectedEmployee != '76-80' && $scope.expectedEmployee != '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 101) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75' || $scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100') && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest >= 51) && ($scope.expectedEmployee == '<20' || $scope.expectedEmployee == '20-40' || $scope.expectedEmployee == '41-75') && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 10 && $scope.user.plantInvest < 100) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['Priority Sector Incentives under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest > 10 && $scope.user.plantInvest < 50) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['Priority Sector Incentives in Backward Districts under IPR 2015'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.districtVal == "A" || $scope.districtVal == "0")) {
					$scope.policyObj = ['MSME Priority Sector Incentives under IPR 2015', 'Under MSME Development policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
				else if (($scope.user.plantInvest < 10) && ($scope.districtVal == "B")) {
					$scope.policyObj = ['MSME Priority Sector Incentives in Backward Districts under IPR 2015', 'Under MSME Development policy 2016'];
					$scope.matchedIncentives($scope.policyObj);
				}
			}


			$scope.employmentIncentives = function () {
				if ($scope.districtVal == "A") {
					if ($scope.user.plantInvest >= 10 && $scope.sectorVal == "RenewableManufacturing") {
						$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
						$scope.empCapSubAmt = ($scope.user.plantInvest * 10) / 100;
						if ($scope.empCapSubAmt <= 10) {
							$scope.empCapSubAmt = $scope.empCapSubAmt;
						}
						else {
							$scope.empCapSubAmt = 10;
						}
						if ($scope.expectedEmployee != '<20') {
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.25;
							$scope.newTraining = 2500;
							$scope.skillUpgradation = 1750;
						}
					}
					else if ($scope.user.plantInvest >= 101 && $scope.user.plantInvest <= 200) {
						$scope.empCapSubAmt = ($scope.user.plantInvest * 10) / 100;
						if ($scope.empCapSubAmt <= 10) {
							$scope.empCapSubAmt = $scope.empCapSubAmt;
						}
						else {
							$scope.empCapSubAmt = 10;
						}
						if ($scope.expectedEmployee == '101-150') {
							$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.25;
							$scope.newTraining = 2500;
							$scope.skillUpgradation = 1750;
						}
						else if ($scope.expectedEmployee == '151-180' || $scope.expectedEmployee == '181-200' || $scope.expectedEmployee == '201-250' || $scope.expectedEmployee == '251-300') {
							$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.35;
							$scope.newTraining = 2750;
							$scope.skillUpgradation = 1900;
						}
						else if ($scope.expectedEmployee == '301-500') {
							$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.45;
							$scope.newTraining = 3000;
							$scope.skillUpgradation = 2100;
						}
						else if ($scope.expectedEmployee == '501-750' || $scope.expectedEmployee == '751-1000' || $scope.expectedEmployee == '1001-1500' || $scope.expectedEmployee == '>1500') {
							$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.5;
							$scope.newTraining = 3300;
							$scope.skillUpgradation = 2400;
						}
					}
					else if ($scope.user.plantInvest >= 201 && $scope.user.plantInvest <= 500) {
						$scope.empCapSubAmt = ($scope.user.plantInvest * 10) / 100;
						if ($scope.empCapSubAmt <= 20) {
							$scope.empCapSubAmt = $scope.empCapSubAmt;
						}
						else {
							$scope.empCapSubAmt = 20;
						}
						if ($scope.expectedEmployee == '201-250') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 20.0 crore</p>";
							$scope.hostelLand = 2;
							$scope.powerTariff = 0.4;
							$scope.newTraining = 3000;
							$scope.skillUpgradation = 2000;
						}
						else if ($scope.expectedEmployee == '251-300' || $scope.expectedEmployee == '301-500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 20.0 crore</p>";
							$scope.hostelLand = 2;
							$scope.powerTariff = 0.5;
							$scope.newTraining = 3250;
							$scope.skillUpgradation = 2250;
						}
						else if ($scope.expectedEmployee == '501-750' || $scope.expectedEmployee == '751-1000') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 20.0 crore</p>";
							$scope.hostelLand = 2;
							$scope.powerTariff = 0.6;
							$scope.newTraining = 3500;
							$scope.skillUpgradation = 2500;
						}
						else if ($scope.expectedEmployee == '1001-1500' || $scope.expectedEmployee == '>1500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 20.0 crore</p>";
							$scope.hostelLand = 2;
							$scope.powerTariff = 0.75;
							$scope.newTraining = 3750;
							$scope.skillUpgradation = 2750;
						}

					}
					else if ($scope.user.plantInvest > 500) {
						$scope.empCapSubAmt = ($scope.user.plantInvest * 10) / 100;
						if ($scope.empCapSubAmt <= 50) {
							$scope.empCapSubAmt = $scope.empCapSubAmt;
						}
						else {
							$scope.empCapSubAmt = 50;
						}
						if ($scope.expectedEmployee == '301-500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 50.0 crore</p>";
							$scope.hostelLand = 3;
							$scope.powerTariff = 0.55;
							$scope.newTraining = 3300;
							$scope.skillUpgradation = 2300;
						}
						else if ($scope.expectedEmployee == '501-750' || $scope.expectedEmployee == '751-1000') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 50.0 crore</p>";
							$scope.hostelLand = 3;
							$scope.powerTariff = 0.65;
							$scope.newTraining = 3600;
							$scope.skillUpgradation = 2600;
						}
						else if ($scope.expectedEmployee == '1001-1500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 50.0 crore</p>";
							$scope.hostelLand = 3;
							$scope.powerTariff = 0.8;
							$scope.newTraining = 3800;
							$scope.skillUpgradation = 2800;
						}
						else if ($scope.expectedEmployee == '>1500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 50.0 crore</p>";
							$scope.hostelLand = 3;
							$scope.powerTariff = 1;
							$scope.newTraining = 4000;
							$scope.skillUpgradation = 3000;
						}

					}
				}
				else if ($scope.districtVal == "B") {
					if ($scope.user.plantInvest > 10 && $scope.sectorVal == "RenewableManufacturing") {
						$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
						$scope.empCapSubAmt = ($scope.user.plantInvest * 10) / 100;
						if ($scope.empCapSubAmt <= 10) {
							$scope.empCapSubAmt = $scope.empCapSubAmt;
						}
						else {
							$scope.empCapSubAmt = 10;
						}
						if ($scope.expectedEmployee != '<20') {
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.3;
							$scope.newTraining = 2600;
							$scope.skillUpgradation = 1750;
						}
					}
					else if ($scope.user.plantInvest >= 51 && $scope.user.plantInvest <= 100) {
						$scope.empCapSubAmt = ($scope.user.plantInvest * 10) / 100;
						if ($scope.empCapSubAmt <= 10) {
							$scope.empCapSubAmt = $scope.empCapSubAmt;
						}
						else {
							$scope.empCapSubAmt = 10;
						}
						if ($scope.expectedEmployee == '76-80' || $scope.expectedEmployee == '81-100') {
							$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.3;
							$scope.newTraining = 2600;
							$scope.skillUpgradation = 1750;
						}
						else if ($scope.expectedEmployee == '101-150' || $scope.expectedEmployee == '151-180' || $scope.expectedEmployee == '181-200') {
							$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.4;
							$scope.newTraining = 2750;
							$scope.skillUpgradation = 2000;
						}
						else if ($scope.expectedEmployee == '201-250' || $scope.expectedEmployee == '251-300') {
							$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.5;
							$scope.newTraining = 3000;
							$scope.skillUpgradation = 2250;
						}
						else if ($scope.expectedEmployee == '301-500' || $scope.expectedEmployee == '501-750' || $scope.expectedEmployee == '751-1000' || $scope.expectedEmployee == '1001-1500' || $scope.expectedEmployee == '>1500') {
							$scope.empCapSubVal = "<p>10% of investment  with a max. of Rs. 10 crore</p>";
							$scope.hostelLand = 1;
							$scope.powerTariff = 0.6;
							$scope.newTraining = 3300;
							$scope.skillUpgradation = 2500;
						}
					}
					else if ($scope.user.plantInvest >= 101 && $scope.user.plantInvest <= 250) {
						$scope.empCapSubAmt = ($scope.user.plantInvest * 10) / 100;
						if ($scope.empCapSubAmt <= 20) {
							$scope.empCapSubAmt = $scope.empCapSubAmt;
						}
						else {
							$scope.empCapSubAmt = 20;
						}
						if ($scope.expectedEmployee == '101-150') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 20.0 crore</p>";
							$scope.hostelLand = 2;
							$scope.powerTariff = 0.45;
							$scope.newTraining = 3000;
							$scope.skillUpgradation = 2000;
						}
						else if ($scope.expectedEmployee == '151-180' || $scope.expectedEmployee == '181-200' || $scope.expectedEmployee == '251-300') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 20.0 crore</p>";
							$scope.hostelLand = 2;
							$scope.powerTariff = 0.6;
							$scope.newTraining = 3250;
							$scope.skillUpgradation = 2250;
						}
						else if ($scope.expectedEmployee == '301-500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 20.0 crore</p>";
							$scope.hostelLand = 2;
							$scope.powerTariff = 0.75;
							$scope.newTraining = 3500;
							$scope.skillUpgradation = 2500;
						}
						else if ($scope.expectedEmployee == '501-750' || $scope.expectedEmployee == '751-1000' || $scope.expectedEmployee == '1001-1500' || $scope.expectedEmployee == '>1500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 20.0 crore</p>";
							$scope.hostelLand = 2;
							$scope.powerTariff = 1;
							$scope.newTraining = 3750;
							$scope.skillUpgradation = 2750;
						}

					}
					else if ($scope.user.plantInvest > 250) {
						$scope.empCapSubAmt = ($scope.user.plantInvest * 10) / 100;
						if ($scope.empCapSubAmt <= 50) {
							$scope.empCapSubAmt = $scope.empCapSubAmt;
						}
						else {
							$scope.empCapSubAmt = 50;
						}
						if ($scope.expectedEmployee == '201-250') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 50.0 crore</p>";
							$scope.hostelLand = 3;
							$scope.powerTariff = 0.65;
							$scope.newTraining = 3500;
							$scope.skillUpgradation = 2300;
						}
						else if ($scope.expectedEmployee == '251-300' || $scope.expectedEmployee == '301-500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 50.0 crore</p>";
							$scope.hostelLand = 3;
							$scope.powerTariff = 0.8;
							$scope.newTraining = 3600;
							$scope.skillUpgradation = 2650;
						}
						else if ($scope.expectedEmployee == '501-750') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 50.0 crore</p>";
							$scope.hostelLand = 3;
							$scope.powerTariff = 1.1;
							$scope.newTraining = 3800;
							$scope.skillUpgradation = 2800;
						}
						else if ($scope.expectedEmployee == '751-1000' || $scope.expectedEmployee == '1001-1500' || $scope.expectedEmployee == '>1500') {
							$scope.empCapSubVal = "<p>10% of investment with a max. of Rs. 50.0 crore</p>";
							$scope.hostelLand = 3;
							$scope.powerTariff = 1.25;
							$scope.newTraining = 4000;
							$scope.skillUpgradation = 3000;
						}

					}
				}

				return [$scope.hostelLand, $scope.newTraining, $scope.skillUpgradation, $scope.powerTariff, $scope.empCapSubVal, $scope.empCapSubAmt];
			}
			// incentive functions starts here
			$scope.radioLblTxt = '';
			$scope.radioLblVal = '';
			$scope.hideObj = [];
			$scope.showHideCell = function (itemTeXt) {
				$scope.hideRom = $scope.hideObj.indexOf(itemTeXt);
				return $scope.hideRom;
			}

			/** */
			$scope.fucn1 = function (index, item) {
				if ($scope.investorCategoryVal == 'SC/ST' || $scope.kbkDistrict || $scope.user.gender == 'female') {
					$scope.radioLblTxt = '<p>33% up to Rs. 3 crore for industries setup by SC, ST, Women or in KBK districts</p>';
					$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 33) / 100;
					if ($scope.capitalInvestSubsidyVal <= 3) {
						$scope.capitalInvestSubsidyVal = $scope.capitalInvestSubsidyVal;
					}
					else {
						$scope.capitalInvestSubsidyVal = 3;
					}
				}
				else {
					$scope.radioLblTxt = '<p>25% up to Rs. 2 crore</p>';
					$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 25) / 100;
					if ($scope.capitalInvestSubsidyVal <= 2) {
						$scope.capitalInvestSubsidyVal = $scope.capitalInvestSubsidyVal;
					}
					else {
						$scope.capitalInvestSubsidyVal = 2;
					}
				}
				$scope.radioLblVal = 'of Rs.' + $scope.capitalInvestSubsidyVal.toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn2 = function (index, item) {
				$scope.radioLblTxt = '<p>Up to 20% of the project cost limited to Rs. 15 crore provided to SPV or as equity participation.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn3 = function (index, item) {
				$scope.radioLblTxt = '<p>Credit linked back ended subsidy @ 35% of the cost of standalone new reefer vehicles/ mobile pre-cooling  vans up to maximum of Rs. 25 lakh.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn4 = function (index, item) {
				$scope.radioLblTxt = '<p>35% of cost of machinery up to Rs. 5 lakh.</p><p>50% of cost of machinery up to Rs. 5 lakh in industrially backward districts KBK.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn5 = function (index, item) {
				$scope.radioLblTxt = '<p>One time grant @50% of the cost up to Rs. 1 crore to University/Institute for courses in Food Processing.</p><p>Rs. 3 lakh per ESDP to Govt. ITIs/Polytechnics for conducting 30 days certification program on Food Processing.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn6 = function (index, item) {
				$scope.radioLblTxt = '<p>Up to Rs. 2 lakh per event for organizing seminar/workshops.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn7 = function (index, item) {
				$scope.radioLblTxt = '<p>Rs. 1500 per worker per month for 36 months, if 90% workers are domicile of Odisha</p>';
				$scope.empLen = 0;
				if ($scope.expectedEmployee == '20-40') {
					$scope.empLen = 30;
				}
				else if ($scope.expectedEmployee == '41-75') {

					$scope.empLen = 58;
				}
				else if ($scope.expectedEmployee == '76-80') {

					$scope.empLen = 78;
				}
				else if ($scope.expectedEmployee == '81-100') {

					$scope.empLen = 90;
				}
				else if ($scope.expectedEmployee == '101-150') {

					$scope.empLen = 125;
				}
				else if ($scope.expectedEmployee == '151-180') {

					$scope.empLen = 165;
				}
				else if ($scope.expectedEmployee == '181-200') {

					$scope.empLen = 190;
				}
				else if ($scope.expectedEmployee == '201-250') {

					$scope.empLen = 225;
				}
				else if ($scope.expectedEmployee == '251-300') {

					$scope.empLen = 275;
				}
				else if ($scope.expectedEmployee == '301-500') {

					$scope.empLen = 400;
				}
				else if ($scope.expectedEmployee == '501-750') {

					$scope.empLen = 625;
				}
				else if ($scope.expectedEmployee == '751-1000') {

					$scope.empLen = 875;
				}
				else if ($scope.expectedEmployee == '1001-1500') {

					$scope.empLen = 1250;
				}
				else if ($scope.expectedEmployee == '>1500') {

					$scope.empLen = 750;
				}
				$scope.apparelAmount = parseInt(($scope.empLen * 1500) * 36);
				$scope.radioLblVal = 'of Rs. ' + $scope.apparelAmount.toFixed(2) + '/-';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn8 = function (index, item) {
				$scope.radioLblTxt = '<p>Capital grant of 20% of project cost upto Rs. 20 crore</p><p>Interest free loan upto 10%, maximum limit of Rs. 10 crore</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn9 = function (index, item) {
				$scope.radioLblTxt = '<p>One time grant up to Rs. 2 crore for setting up or upgrading Biotechnology Centre of Excellences, Biotech Enterprise and Skill Development Schools, and Incubators.</p><p>Collaborative research grant up to Rs. 30 lakh per annum per project.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn10 = function (index, item) {
				$scope.radioLblTxt = '<p>Special funding support maximum up to Rs.50.00 lakhs per annum to University/Institutes/Agencies/Industries</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn11 = function (index, item) {
				$scope.radioLblTxt = '<p>30% up to Rs. 10 crore for Grade 1 (minimum 50 beds).</p><p>Additional ceiling of Rs. 5 crore for higher grade.</p>';
				if ($scope.user.totalBed <= 50) {
					$scope.hideObj.push(item);
				}
				else {
					$scope.bedCapital = ($scope.user.plantInvest * 30) / 100;
					$scope.gradeVal = Math.floor($scope.user.totalBed / 100);
					$scope.maxCap = 10;
					if ($scope.gradeVal > 1) {
						$scope.maxCap = 10;
						$scope.maxCap = parseInt($scope.maxCap + (5 * Math.floor($scope.gradeVal - 1)));
					}
					if ($scope.bedCapital < $scope.maxCap) {
						$scope.bedCapital = $scope.bedCapital;
					}
					else {
						$scope.bedCapital = $scope.maxCap;
					}
					$scope.radioLblVal = " of Rs. " + $scope.bedCapital.toFixed(2) + " cr.";
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn17 = function (index, item) {
				$scope.radioLblTxt = '<p>Minimum 40 employees for a maximum of 2 years, subsidy on lease rentals varies from 60% to 80% based on the area.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn18 = function (index, item) {
				$scope.radioLblTxt = '<p>Minimum 40 employees and 2 years in operation, subsidy on lease rentals varies from 30% to 50% based on the area.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn19 = function (index, item) {
				$scope.radioLblTxt = '<p>0.5 acre/50 employees with minimum 2 years of operations.</p><p>acre/100 employees with minimum 2 years of operations.</p><p>>1.0 acre/100 employees with minimum 3 years of operations</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn20 = function (index, item) {
				$scope.radioLblTxt = '<p>5% per annum on term loan for a period of 5 years for Micro & Small Enterprises with a ceiling of Rs. 10 Lakhs and 20 Lakhs respectively.</p>';
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					if ($scope.user.plantInvest <= $scope.microEnterprises) {
						$scope.ceilingAmt = 0.10;
					}
					else {
						$scope.ceilingAmt = 0.20;
					}
					$scope.eligibleLoanAmount = ($scope.user.totalInvestment * 60) / 100;
					$scope.eligibleAmount = ($scope.eligibleLoanAmount * 5) / 100;
					$scope.eligibleText = '';
					if ($scope.eligibleAmount < $scope.ceilingAmt) {
						for (i = 0; i < 5; i++) {
							$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 1) + ",";
							if ((1 - ($scope.eligibleAmount * (i + 1))) < $scope.eligibleAmount) {
								$scope.eligibleAmount = $scope.ceilingAmt - ($scope.eligibleAmount * (i + 1));
								$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 2) + ".";
								break;
							}
						}
					}
					else {
						$scope.eligibleAmount = $scope.ceilingAmt;
						$scope.eligibleText = " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year 1.";
					}
					$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				}
				else {
					if($scope.sectorVal == "ESDM" && $scope.user.plantInvest >= 50 && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75')){
						$scope.radioLblTxt = '<p>At 5% per annum for a period of 5 years with a maximum moratorium of 18 months.</p>';
						$scope.radioLblVal = "At 5% per annum for a period of 5 years with a maximum moratorium of 18 months.";
					}
					else {
						$scope.hideObj.push(item);
					}
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn21 = function (index, item) {
				if($scope.sectorVal == "ESDM" && $scope.user.plantInvest >= 50 && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75')){
					$scope.radioLblTxt = '<p>IT/ITES-20% on FCI with a maximum limit of Rs. 50 Lakhs.ESDM -20% on FCI with a maximum limit of Rs. 500 Lakhs And 25% on Fixed Capital Investment for plant and machinery subject to maximum of 0.83 crore</p>';
				}
				else {
					$scope.radioLblTxt = '<p>IT/ITES-20% on FCI with a maximum limit of Rs. 50 Lakhs.</p><p>ESDM -20% on FCI with a maximum limit of Rs. 500 Lakhs</p>';
				}
				if ($scope.sectorVal == "IT/ITES") {
					$scope.maxLimit = 0.5;
					$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 20) / 100;
				}
				else if ($scope.sectorVal == "ESDM") {
					if($scope.user.plantInvest >= 50 && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75')){
						$scope.maxLimit = 5.83;
						$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 25) / 100;
					}
					else {
						$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 20) / 100;
						$scope.maxLimit = 5;
					}
				}
				if ($scope.capitalInvestSubsidyVal <= $scope.maxLimit) {
					$scope.capitalInvestSubsidyVal = $scope.capitalInvestSubsidyVal;
				}
				else {
					$scope.capitalInvestSubsidyVal = $scope.maxLimit;
				}
				$scope.radioLblVal = 'of Rs.' + $scope.capitalInvestSubsidyVal.toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn22 = function (index, item) {
				$scope.radioLblTxt = '<p>Exemption from Electricity duty and inspection fee.</p><p>Uninterrupted power</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn23 = function (index, item) {
				$scope.eligibleText = "";
				$scope.turnover = ($scope.user.totalInvestment * 4);
				$scope.vatMaxVal = $scope.user.plantInvest;
				$scope.vatVal = ($scope.turnover * 12.5) / 100;
				if($scope.sectorVal == "ESDM" && $scope.user.plantInvest >= 50 && ($scope.expectedEmployee != '<20' && $scope.expectedEmployee != '20-40' && $scope.expectedEmployee != '41-75')){
					$scope.radioLblTxt = '<p>100% reimbursement for a period of 5 years limited to 100% of the fixed capital investment.</p>';
					$scope.vatVal = $scope.vatVal;
				}
				else {
					$scope.radioLblTxt = '<p>75% reimbursement for a period of 5 years limited to 100% of Fixed Capital Investment.</p>';
					$scope.vatVal = ($scope.vatVal * 75) / 100;
				}
				if ($scope.vatVal < $scope.vatMaxVal) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.vatMaxVal - ($scope.vatVal * (i + 1))) < $scope.vatVal) {
							$scope.vatVal = $scope.vatMaxVal - ($scope.vatVal * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.vatVal = $scope.vatMaxVal;
					$scope.eligibleText = " Rs. " + $scope.vatVal + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn24 = function (index, item) {
				$scope.radioLblTxt = '<p>Entry Tax Exemption (during construction)- for a period of 3 years 10% of area for Incubation Units</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			/** */
			$scope.fucn25 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					if ($scope.investorCategoryVal == 'SC/ST' || $scope.investorCategoryVal == 'differentlyAbled' || $scope.user.gender == 'female') {
						if ($scope.kbkDistrict || $scope.districtVal == "B") {
							$scope.capitalSubsidyVal = ($scope.user.plantInvest * 35) / 100;
							$scope.radioLblTxt = '<p>35% for Micro & Small up to Rs. 1.25 crore</p>';
						}
						else {
							$scope.capitalSubsidyVal = ($scope.user.plantInvest * 30) / 100;
							$scope.radioLblTxt = '<p>30% for Micro & Small up to Rs. 1.25 crore</p>';
						}
						if ($scope.capitalSubsidyVal <= 1.25) {
							$scope.capitalSubsidyVal = $scope.capitalSubsidyVal;
						}
						else {
							$scope.capitalSubsidyVal = 1.25;
						}
					}
					else {
						if ($scope.kbkDistrict || $scope.districtVal == "B") {
							$scope.capitalSubsidyVal = ($scope.user.plantInvest * 30) / 100;
							$scope.radioLblTxt = '<p>30% for Micro & Small up to Rs. 1 crore</p>';
						}
						else {
							$scope.capitalSubsidyVal = ($scope.user.plantInvest * 25) / 100;
							$scope.radioLblTxt = '<p>25% for Micro & Small up to Rs. 1 crore</p>';
						}
						if ($scope.capitalSubsidyVal <= 1) {
							$scope.capitalSubsidyVal = $scope.capitalSubsidyVal;
						}
						else {
							$scope.capitalSubsidyVal = 1;
						}
					}
					$scope.radioLblVal = 'of Rs.' + $scope.capitalSubsidyVal.toFixed(2) + ' cr.';
				}
				else {
					$scope.hideObj.push(item);
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn26 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises && $scope.investorCategoryVal == 'SC/ST' || $scope.investorCategoryVal == 'differentlyAbled' || $scope.user.gender == 'female' || $scope.kbkDistrict || $scope.districtVal == "B") {
					$scope.termLoan = ($scope.user.totalInvestment * 60) / 100;
					$scope.seedVal = ($scope.termLoan * 10) / 100;
					if ($scope.seedVal <= 0.15) {
						$scope.seedVal = $scope.seedVal;
					}
					else {
						$scope.seedVal = 0.15;
					}
					$scope.radioLblTxt = '<p>One time grant of 10% of term loan disbursed up to Rs. 15 lakh for Micro & Small unit owned by 1st generation SC, ST, Differently abled or women entrepreneur in industrially backward districts including KBK</p>';
					$scope.radioLblVal = 'Seed Capital Grant of Rs.' + $scope.seedVal.toFixed(2) + ' cr.';
				}
				else {
					$scope.hideObj.push(item);
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn27 = function (index, item) {
				if ((($scope.user.totalInvestment * 2) / 100) <= 0.05) {
					$scope.projectReportSubsidy = ($scope.user.totalInvestment * 2) / 100;
				}
				else {
					$scope.projectReportSubsidy = 0.05;
				}
				$scope.radioLblTxt = '<p>One time grant of Rs. 50,000 or 2% of project cost, whichever lower for preparation of DPR/detailed feasibility report</p>';
				$scope.radioLblVal = 'of Rs.' + $scope.projectReportSubsidy.toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn28 = function (index, item) {
				$scope.radioLblTxt = '<p>Onetime reimbursement of 50% of audit cost up to Rs. 25,000</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn29 = function (index, item) {
				$scope.radioLblTxt = '<p>Onetime grant of 20% of expenditure in raising capital up to Rs. 10 lakh after successfully raising the equity</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn30 = function (index, item) {
				$scope.radioLblTxt = '<p>50% of expenditure up to Rs. 25,000</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn31 = function (index, item) {
				$scope.radioLblTxt = '<p>50% of skill upgradation or training for local manpower up to Rs. 3000 per person for maximum 10 persons in micro and 20 persons in Small and Medium enterprises. Additional Rs. 1000 per women trained</p>';
				$scope.empMentVal = $scope.employmentIncentives();
				$scope.empLen = 0;
				if ($scope.expectedEmployee == '20-40') {
					$scope.empLen = 30;
				}
				else if ($scope.expectedEmployee == '41-75') {

					$scope.empLen = 58;
				}
				else if ($scope.expectedEmployee == '76-80') {

					$scope.empLen = 78;
				}
				else if ($scope.expectedEmployee == '81-100') {

					$scope.empLen = 90;
				}
				else if ($scope.expectedEmployee == '101-150') {

					$scope.empLen = 125;
				}
				else if ($scope.expectedEmployee == '151-180') {

					$scope.empLen = 165;
				}
				else if ($scope.expectedEmployee == '181-200') {

					$scope.empLen = 190;
				}
				else if ($scope.expectedEmployee == '201-250') {

					$scope.empLen = 225;
				}
				else if ($scope.expectedEmployee == '251-300') {

					$scope.empLen = 275;
				}
				else if ($scope.expectedEmployee == '301-500') {

					$scope.empLen = 400;
				}
				else if ($scope.expectedEmployee == '501-750') {

					$scope.empLen = 625;
				}
				else if ($scope.expectedEmployee == '751-1000') {

					$scope.empLen = 875;
				}
				else if ($scope.expectedEmployee == '1001-1500') {

					$scope.empLen = 1250;
				}
				else if ($scope.expectedEmployee == '>1500') {

					$scope.empLen = 750;
				}
				$scope.newEmpLen = ($scope.empLen * 40) / 100;
				$scope.skillEmpLen = ($scope.empLen * 30) / 100;
				$scope.totalEmpForReimburs = Math.floor(($scope.newEmpLen + $scope.skillEmpLen) / 2);
				if ($scope.user.plantInvest <= $scope.microEnterprises) {
					$scope.maxPerson = 10;
				}
				else if ($scope.user.plantInvest > $scope.microEnterprises && $scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.maxPerson = 20;
				}
				if ($scope.totalEmpForReimburs <= $scope.maxPerson) {
					$scope.totalEmpForReimburs = $scope.totalEmpForReimburs;
				} else {
					$scope.totalEmpForReimburs = $scope.maxPerson;
				}

				$scope.totalTrainingVal = ($scope.totalEmpForReimburs * 3000);
				if($scope.totalTrainingVal.toFixed(2) == 0) {
					$scope.radioLblVal = 'Rs. 1000 per women trained.';
				}
				else {
					$scope.radioLblVal = 'of Rs.' + $scope.totalTrainingVal.toFixed(2) + '/- and Additional Rs. 1000 per women trained.';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn32 = function (index, item) {
				$scope.radioLblTxt = '<p>Annual State awards  for best MSME/ entrepreneurs based on various criteria</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn33 = function (index, item) {
				$scope.radioLblTxt = '<p>50% of the infrastructure cost as grant for clusters up to Rs. 10 crore per park/cluster</p><p>50% up to Rs. 5 crore for upgradation of existing park/cluster</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn34 = function (index, item) {
				$scope.radioLblTxt = '<p>A0 – Situated in A districts as per IPR, and with investment more than Rs. 20 crore with minimum employment of 80.</p><p>B0 – Situated in B districts as per IPR, and with investment more than Rs. 10 crore with minimum employment of 40</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn35 = function (index, item) {
				$scope.radioLblTxt = '<p>For A0/B0 – 10% of cost of plant and machinery</p>';
				if ($scope.pharmaCapitalSubsidy) {
					$scope.radioLblVal = 'of Rs.' + (($scope.user.plantInvest * 10) / 100).toFixed(2) + ' cr.';
				}
				else {
					$scope.hideObj.push(item);
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn36 = function (index, item) {
				$scope.radioLblTxt = '<p>Rs. 50 lakh or 20% of capital cost, whichever is less, for adopting zero effluent/waste water discharge units</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn37 = function (index, item) {
				$scope.radioLblTxt = '<p>No stamp duty for land allotted by the Government/IDCO to solar park developers</p><p>Exemption from electricity duty for self-consumption for 5 years</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn38 = function (index, item) {
				$scope.radioLblTxt = '<p>Testing charges of EIC would be waived off.</p><p>Supervision charges shall not be levied by DISCOM/OPTCL.</p><p>No clearance from OSPCB would be required for projects except Biomass and Municipal Solid Waste Projects.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			// $scope.fucn39 = function (index, item) {
			// 	$scope.radioLblTxt = '<p>To be treated as Priority Sector under IPR, 2015</p><p>Minimum investment of Rs. 10 crore and employment of 20, to receive benefits of Category ‘A1’ or ‘B1’, depending on the location, from IPR, 2015</p>';
			// 	$scope.radioLblVal = '';

			// 	return [$scope.radioLblTxt,$scope.radioLblVal];
			// }
			$scope.fucn40 = function (index, item) {
				if ($scope.user.plantInvest >= 0.2 && $scope.user.plantInvest < 50) {
					if ($scope.enterpriseVal == 'tentedAccommodation') {
						$scope.radioLblTxt = '<p>Tented accommodation with minimum Rs. 0.20 crore with a maximum limit of Rs. 0.15 crore.</p>';
						$scope.maxLimit = 0.15;
						if ($scope.investorCategoryVal == 'SC/ST' || $scope.investorCategoryVal == 'differentlyAbled' || $scope.kbkDistrict || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'PURI' || $scope.districtName == 'GANJAM' || $scope.districtName == 'KHORDHA' || $scope.user.gender == 'female') {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 35) / 100;
						}
						else {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 30) / 100;
						}
					}
					else if ($scope.enterpriseVal == 'equipmentWorth') {
						$scope.radioLblTxt = '<p>Equipment worth Rs. 1 crore or more for Adventure & Water sports.</p>';
						$scope.maxLimit = 0.50;
						if ($scope.investorCategoryVal == 'SC/ST' || $scope.investorCategoryVal == 'differentlyAbled' || $scope.kbkDistrict || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'PURI' || $scope.districtName == 'GANJAM' || $scope.districtName == 'KHORDHA' || $scope.user.gender == 'female') {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 25) / 100;
						}
						else {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 20) / 100;
						}
					}
					else {
						$scope.radioLblTxt = '<p>Rs.20 lakh – 50 Cr. with a maximum limit of Rs. 10 crore.</p>';
						$scope.maxLimit = 10;
						if ($scope.investorCategoryVal == 'SC/ST' || $scope.investorCategoryVal == 'differentlyAbled' || $scope.kbkDistrict || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'PURI' || $scope.districtName == 'GANJAM' || $scope.districtName == 'KHORDHA' || $scope.user.gender == 'female') {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 25) / 100;
						}
						else {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 20) / 100;
						}
					}
				}
				else if ($scope.user.plantInvest >= 50) {
					if ($scope.enterpriseVal == 'tentedAccommodation') {
						$scope.radioLblTxt = '<p>Tented accommodation with minimum Rs. 0.20 crore with a maximum limit of Rs. 0.15 crore.</p>';
						$scope.maxLimit = 0.15;
						if ($scope.investorCategoryVal == 'SC/ST' || $scope.investorCategoryVal == 'differentlyAbled' || $scope.kbkDistrict || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'PURI' || $scope.districtName == 'GANJAM' || $scope.districtName == 'KHORDHA' || $scope.user.gender == 'female') {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 35) / 100;
						}
						else {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 30) / 100;
						}
					}
					else if ($scope.enterpriseVal == 'equipmentWorth') {
						$scope.radioLblTxt = '<p>Equipment worth Rs. 1 crore or more for Adventure & Water sports.</p>';
						$scope.maxLimit = 0.50;
						if ($scope.investorCategoryVal == 'SC/ST' || $scope.investorCategoryVal == 'differentlyAbled' || $scope.kbkDistrict || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'PURI' || $scope.districtName == 'GANJAM' || $scope.districtName == 'KHORDHA' || $scope.user.gender == 'female') {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 25) / 100;
						}
						else {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 20) / 100;
						}
					}
					else {
						$scope.radioLblTxt = '<p>Rs.50 Cr. & above with a maximum limit of Rs. 15 crore.</p>';
						$scope.maxLimit = 15;
						if ($scope.investorCategoryVal == 'SC/ST' || $scope.investorCategoryVal == 'differentlyAbled' || $scope.kbkDistrict || $scope.districtName == 'GAJAPATI' || $scope.districtName == 'KANDHAMAL' || $scope.districtName == 'PURI' || $scope.districtName == 'GANJAM' || $scope.districtName == 'KHORDHA' || $scope.user.gender == 'female') {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 25) / 100;
						}
						else {
							$scope.capitalInvestSubsidyVal = ($scope.user.plantInvest * 20) / 100;
						}
					}
				}

				if ($scope.capitalInvestSubsidyVal <= $scope.maxLimit) {
					$scope.capitalInvestSubsidyVal = $scope.capitalInvestSubsidyVal;
				}
				else {
					$scope.capitalInvestSubsidyVal = $scope.maxLimit;
				}
				$scope.radioLblVal = 'of Rs.' + $scope.capitalInvestSubsidyVal.toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn41 = function (index, item) {
				$scope.radioLblTxt = '<p>5% per annum up to Rs. 1 crore for 5 years</p>';
				$scope.eligibleAmount = ($scope.user.totalInvestment * 5) / 100;
				$scope.eligibleText = '';
				if ($scope.eligibleAmount < 1) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if ((1 - ($scope.eligibleAmount * (i + 1))) < $scope.eligibleAmount) {
							$scope.eligibleAmount = 1 - ($scope.eligibleAmount * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.eligibleAmount = 1;
					$scope.eligibleText = " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn42 = function (index, item) {
				$scope.radioLblTxt = '<p>100% exemption on purchase of land for the project</p>';
				if ($scope.districtVal == "A") {
					$scope.stampDutyVal = (1.3 * 5) / 100;
				}
				else {
					$scope.stampDutyVal = (0.3 * 5) / 100;
				}
				$scope.radioLblVal = 'of Rs.' + $scope.stampDutyVal.toFixed(2) + ' cr. per acres.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn43 = function (index, item) {
				$scope.radioLblTxt = '<p>100% reimbursement</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn44 = function (index, item) {
				$scope.radioLblTxt = '<p>100% for a period of 7 years limited to 200% of cost of plant and machinery/ 100% of project cost, whichever is lower</p>';
				$scope.eligibleText = "";
				$scope.turnover = ($scope.user.plantInvest * 4);
				$scope.vatVal = ($scope.turnover * 12.5) / 100;
				for (i = 0; i < 7; i++) {
					if (i == 6) {
						$scope.eligibleText += " and Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ".";
					}
					else {
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ",";
					}
					if (($scope.user.plantInvest - ($scope.vatVal * (i + 1))) < $scope.vatVal) {
						$scope.vatVal = $scope.user.plantInvest - ($scope.vatVal * (i + 1));
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 2) + ".";
						break;
					}
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn45 = function (index, item) {
				$scope.radioLblTxt = '<p>100% for a period of 5 years for New Multiplex Cinema halls of at least 3 screens with investment of Rs. 3 crore, Public Aquarium, Aqua park & Amusement Park</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn46 = function (index, item) {
				$scope.radioLblTxt = '<p>Exemption of electricity duty up to a contract demand of 5MVA for 5 years</p><p>Onetime reimbursement of energy audit cost up to Rs. 1 lakh</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn47 = function (index, item) {
				return $scope.fucn98(index, item);
			}
			$scope.fucn48 = function (index, item) {
				$scope.radioLblTxt = '<p>75% for male workers for 5 years</p><p>100% for female workers for 5 years</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn49 = function (index, item) {
				$scope.radioLblTxt = '<p>20% of the capital cost of setting up effluent treatment plant/sewerage treatment plant up to Rs. 20 lakh</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn50 = function (index, item) {
				$scope.radioLblTxt = '<p>Rs. 2000 per employee trained within 3 years</p>';
				$scope.empLen = 0;
				if ($scope.expectedEmployee == '20-40') {
					$scope.empLen = 30;
				}
				else if ($scope.expectedEmployee == '41-75') {

					$scope.empLen = 58;
				}
				else if ($scope.expectedEmployee == '76-80') {

					$scope.empLen = 78;
				}
				else if ($scope.expectedEmployee == '81-100') {

					$scope.empLen = 90;
				}
				else if ($scope.expectedEmployee == '101-150') {

					$scope.empLen = 125;
				}
				else if ($scope.expectedEmployee == '151-180') {

					$scope.empLen = 165;
				}
				else if ($scope.expectedEmployee == '181-200') {

					$scope.empLen = 190;
				}
				else if ($scope.expectedEmployee == '201-250') {

					$scope.empLen = 225;
				}
				else if ($scope.expectedEmployee == '251-300') {

					$scope.empLen = 275;
				}
				else if ($scope.expectedEmployee == '301-500') {

					$scope.empLen = 400;
				}
				else if ($scope.expectedEmployee == '501-750') {

					$scope.empLen = 625;
				}
				else if ($scope.expectedEmployee == '751-1000') {

					$scope.empLen = 875;
				}
				else if ($scope.expectedEmployee == '1001-1500') {

					$scope.empLen = 1250;
				}
				else if ($scope.expectedEmployee == '>1500') {

					$scope.empLen = 750;
				}
				$scope.totalEmpforSubsidy = ($scope.empLen * 70) / 100;
				$scope.totalSubAmt = $scope.totalEmpforSubsidy * 2000;
				$scope.yearlySubAmt = $scope.totalSubAmt / 3;
				$scope.radioLblVal = 'Rs. ' + $scope.totalSubAmt.toFixed(2) + '/- @ Rs. ' + $scope.yearlySubAmt.toFixed(2) + '/- per Annum.';

				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn51 = function (index, item) {
				$scope.radioLblTxt = '<p>75% exemption from registration charges and 50% concession from payment of permit charges for AC coaches with minimum 25 seats</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn52 = function (index, item) {
				$scope.radioLblTxt = '<p>50% of the space rent and travel expenses to a maximum of Rs. 75000</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn53 = function (index, item) {
				$scope.radioLblTxt = '<p>75% of actual expenditure in accommodation in Odisha to be reimbursed up to Rs. 10 lakh</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn54 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>100% up to 5 acres</p>';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '<p>75% up to 25 acres</p>';
				}
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn55 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of seven years, to a maximum of Rs. 20/10 lakh</p>';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of seven years, to a maximum of Rs. 40 lakh</p>';
				}
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn56 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>Reimbursed completely</p>';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '';
					$scope.hideObj.push(item);
				}
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn57 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>75% when transfer of land/shed by Govt, IDCO and Private Industrial Estate developer to industrial units.</p>';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '<p>50 % when transfer of land/shed by Govt, IDCO and Private Industrial Estate developer to industrial units.</p>';
				}
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn58 = function (index, item) {
				$scope.radioLblTxt = '<p>Up to a contract demand of 500 KVA  for a period of 5 years</p><p>100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years</p>';
				if ($scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblVal = '5% of the energy charge exempted up to a contract demand of 500 KVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				else {
					$scope.radioLblVal = '8% of the energy charge exempted up to a contract demand of 500 KVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn59 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>To a maximum of Rs. 2/1 lakh per unit subject to achieving energy efficiency.</p>';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '<p>To a maximum of Rs. 3 lakh per unit subject to achieving energy efficiency.</p>';
				}
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn60 = function (index, item) {
				$scope.radioLblTxt = '<p>75% for a period of 5 years, to a maximum of 100% of cost of plant and machinery.</p>';
				$scope.eligibleText = "";
				$scope.turnover = ($scope.user.totalInvestment * 4);
				$scope.vatMaxVal = $scope.user.plantInvest;
				$scope.vatVal = ($scope.turnover * 12.5) / 100;
				$scope.vatVal = ($scope.vatVal * 75) / 100;
				if ($scope.vatVal < $scope.vatMaxVal) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.vatMaxVal - ($scope.vatVal * (i + 1))) < $scope.vatVal) {
							$scope.vatVal = $scope.vatMaxVal - ($scope.vatVal * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.vatVal = $scope.vatMaxVal;
					$scope.eligibleText = " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn61 = function (index, item) {
				return $scope.fucn98(index, item);
			}
			$scope.fucn62 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn63 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of registration cost to a maximum of Rs. 10 lakhs</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn64 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of charges for a period of 3 years, to a maximum of Rs. 3 lakhs</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn65 = function (index, item) {
				$scope.radioLblTxt = '<p>100% reimbursement of cost for purchase up to Rs. 1 lakh for indigenous technology and Rs. 5 lakh for imported</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn66 = function (index, item) {
				$scope.radioLblTxt = '<p>20 lakhs or 20% of capital cost for setting up of Effluent Treatment Plant</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn67 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>100% up to 5 acres</p>';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '<p>75% up to 25 acres</p>';
				}
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn68 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of five years, to a maximum of Rs. 20/10 lakh</p>';
					$scope.maxAmt = 0.20;
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of five years, to a maximum of Rs. 40 lakh</p>';
					$scope.maxAmt = 0.40;
				}
				$scope.eligibleLoanAmount = ($scope.user.totalInvestment * 60) / 100;
				$scope.eligibleAmount = ($scope.eligibleLoanAmount * 5) / 100;
				$scope.eligibleText = '';
				if ($scope.eligibleAmount < $scope.maxAmt) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.maxAmt - ($scope.eligibleAmount * (i + 1))) < $scope.eligibleAmount) {
							$scope.eligibleAmount = $scope.maxAmt - ($scope.eligibleAmount * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.eligibleAmount = $scope.maxAmt;
					$scope.eligibleText = " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn69 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>Reimbursed completely</p>';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '';
					$scope.hideObj.push(item);
				}
				$scope.radioLblVal = 'is Reimbursed completely.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn70 = function (index, item) {
				if ($scope.districtVal == "A") {
					$scope.stampDutyVal = (1.3 * 5) / 100;
				}
				else {
					$scope.stampDutyVal = (0.3 * 5) / 100;
				}
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>75% when transfer of land/shed by Govt, IDCO and Private Industrial Estate developer to industrial units</p>';
					$scope.radioLblVal = 'of Rs. ' + (($scope.user.stampDutyVal * 75) / 100).toFixed(2) + ' cr. per acres.';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '<p>50 % when transfer of land/shed by Govt, IDCO and Private Industrial Estate developer to industrial units</p>';
					$scope.radioLblVal = 'of Rs. ' + (($scope.user.stampDutyVal * 50) / 100).toFixed(2) + ' cr. per acres.';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn71 = function (index, item) {
				$scope.radioLblTxt = '<p>Up to a contract demand of 500 KVA  for a period of 5 years</p><p>100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years</p>';
				if ($scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblVal = '5% of the energy charge exempted up to a contract demand of 500 KVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				else {
					$scope.radioLblVal = '8% of the energy charge exempted up to a contract demand of 500 KVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn72 = function (index, item) {
				if ($scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.radioLblTxt = '<p>To a maximum of Rs. 2/1 lakh per unit subject to achieving energy efficiency.</p>';
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblTxt = '<p>To a maximum of Rs. 3 lakh per unit subject to achieving energy efficiency.</p>';
				}
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn73 = function (index, item) {
				$scope.radioLblTxt = '<p>75% for a period of 5 years, to a maximum of 100% of cost of plant and machinery.</p>';
				$scope.eligibleText = "";
				$scope.turnover = ($scope.user.totalInvestment * 4);
				$scope.vatMaxVal = $scope.user.plantInvest;
				$scope.vatVal = ($scope.turnover * 12.5) / 100;
				$scope.vatVal = ($scope.vatVal * 75) / 100;
				if ($scope.vatVal < $scope.vatMaxVal) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.vatMaxVal - ($scope.vatVal * (i + 1))) < $scope.vatVal) {
							$scope.vatVal = $scope.vatMaxVal - ($scope.vatVal * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.vatVal = $scope.vatMaxVal;
					$scope.eligibleText = " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn74 = function (index, item) {
				return $scope.fucn98(index, item);
			}
			$scope.fucn75 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn76 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of registration cost to a maximum of Rs. 10 lakhs.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn77 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of charges for a period of 3 years, to a maximum of Rs. 3 lakhs.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn78 = function (index, item) {
				$scope.radioLblTxt = '<p>100% reimbursement of cost for purchase up to Rs. 1 lakh for indigenous technology and Rs. 5 lakh for imported.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn79 = function (index, item) {
				$scope.radioLblTxt = '<p>20 lakhs or 20% of capital cost for setting up of Effluent Treatment Plant.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}

			/** */
			$scope.fucn80 = function (index, item) {
				return $scope.fucn94(index, item);
			}
			$scope.fucn81 = function (index, item) {
				return $scope.fucn95(index, item);
			}
			$scope.fucn82 = function (index, item) {
				return $scope.fucn96(index, item);
			}
			$scope.fucn83 = function (index, item) {
				return $scope.fucn97(index, item);
			}
			$scope.fucn84 = function (index, item) {
				return $scope.fucn98(index, item);
			}
			$scope.fucn85 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.hideObj.push(item);
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn86 = function (index, item) {
				return $scope.fucn100(index, item);
			}
			$scope.fucn87 = function (index, item) {
				return $scope.fucn101(index, item);
			}
			$scope.fucn88 = function (index, item) {
				return $scope.fucn102(index, item);
			}
			$scope.fucn89 = function (index, item) {
				return $scope.fucn103(index, item);
			}
			$scope.fucn90 = function (index, item) {
				return $scope.fucn104(index, item);
			}
			$scope.fucn91 = function (index, item) {
				return $scope.fucn105(index, item);
			}
			$scope.fucn92 = function (index, item) {
				return $scope.fucn106(index, item);
			}
			$scope.fucn93 = function (index, item) {
				return $scope.fucn107(index, item);
			}
			$scope.fucn94 = function (index, item) {
				$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of five years, to a maximum of Rs. 10/20/40 lakhs for Micro/Small/Medium industries respectively.</p>';
				if ($scope.user.plantInvest <= $scope.microEnterprises) {
					$scope.maxAmt = 0.10;
				}
				else if ($scope.user.plantInvest > $scope.microEnterprises && $scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.maxAmt = 0.20;
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.maxAmt = 0.30;
				}
				$scope.eligibleLoanAmount = ($scope.user.totalInvestment * 60) / 100;
				$scope.eligibleAmount = ($scope.eligibleLoanAmount * 5) / 100;
				$scope.eligibleText = '';
				if ($scope.eligibleAmount < $scope.maxAmt) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.maxAmt - ($scope.eligibleAmount * (i + 1))) < $scope.eligibleAmount) {
							$scope.eligibleAmount = $scope.maxAmt - ($scope.eligibleAmount * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.eligibleAmount = $scope.maxAmt;
					$scope.eligibleText = " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn95 = function (index, item) {
				$scope.radioLblTxt = '<p>100% with respect to land allotted by the Government to IDCO or Govt/IDCO to Private Industrial Estate Developers.</p><p>100% when transfer of land/shed by Govt, IDCO and Private Industrial Estate developer to industrial units.</p>';
				if ($scope.districtVal == "A") {
					$scope.stampDutyVal = (1.3 * 5) / 100;
				}
				else {
					$scope.stampDutyVal = (0.3 * 5) / 100;
				}
				$scope.radioLblVal = 'of Rs.' + $scope.stampDutyVal.toFixed(2) + ' cr. per acres.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn96 = function (index, item) {
				$scope.radioLblTxt = '<p>Up to a contract demand of 5 MVA  for a period of 5 years.</p><p>100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years.</p>';
				if ($scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblVal = '5% of the energy charge exempted up to a contract demand of 5 MVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				else {
					$scope.radioLblVal = '8% of the energy charge exempted up to a contract demand of 5 MVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn97 = function (index, item) {
				$scope.radioLblTxt = '<p>100% for a period of 7 years, to a maximum of 200% of cost of plant and machinery.</p>';
				$scope.eligibleText = "";
				$scope.turnover = ($scope.user.totalInvestment * 4);
				$scope.vatMaxVal = $scope.user.plantInvest * 2;
				$scope.vatVal = ($scope.turnover * 12.5) / 100;
				if ($scope.vatVal < $scope.vatMaxVal) {
					for (i = 0; i < 7; i++) {
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.vatMaxVal - ($scope.vatVal * (i + 1))) < $scope.vatVal) {
							$scope.vatVal = $scope.vatMaxVal - ($scope.vatVal * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.vatVal = $scope.vatMaxVal;
					$scope.eligibleText = " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn98 = function (index, item) {
				$scope.radioLblTxt = '<p>100% on acquisition of plant & machinery.</p>';
				if ($scope.sectorVal == "IT/ITES" || $scope.sectorVal == "Tourism") {
					$scope.entryTax = 1;
				}
				else {
					$scope.entryTax = 2;
				}
				$scope.radioLblVal = 'of Rs. ' + (($scope.user.plantInvest * $scope.entryTax) / 100).toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn99 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.hideObj.push(item);
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn100 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of registration cost to a maximum of Rs. 10 lakhs.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn101 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of charges for a period of 3 years, to a maximum of Rs. 3 lakhs.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn102 = function (index, item) {
				$scope.radioLblTxt = '<p>100% reimbursement of cost for purchase up to Rs. 1 lakh for indigenous technology and Rs. 5 lakh for imported.</p>';
				$scope.radioLblVal = '';
				// $scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn103 = function (index, item) {
				$scope.radioLblTxt = '<p>25% subsidy on cost of land.</p><p>VAT Reimbursement for additional 2 years subject to the overall limit.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn104 = function (index, item) {
				$scope.radioLblTxt = '<p>10% of the land for large projects earmarked for Ancillary and Downstream park, up to 300 acres.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn105 = function (index, item) {
				$scope.radioLblTxt = '<p>50% of the infrastructure cost with a ceiling of Rs.10 crore per green field industrial park/cluster.</p><p>50% of total cost with a ceiling of Rs.5 crore for up gradation of brown field clusters.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn106 = function (index, item) {
				$scope.radioLblTxt = '<p>Government shall provide exclusive sub-station for industrial park with energy requirement in excess of 20 MVA.</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn107 = function (index, item) {
				$scope.radioLblTxt = '<p>100% up to 100 acres and 50% for balance area.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}


			/**/
			$scope.fucn108 = function (index, item) {
				return $scope.fucn121(index, item);
			}
			$scope.fucn109 = function (index, item) {
				return $scope.fucn122(index, item);
			}
			$scope.fucn110 = function (index, item) {
				return $scope.fucn123(index, item);
			}
			$scope.fucn111 = function (index, item) {
				return $scope.fucn124(index, item);
			}
			$scope.fucn112 = function (index, item) {
				return $scope.fucn125(index, item);
			}
			$scope.fucn113 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.hideObj.push(item);
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn114 = function (index, item) {
				return $scope.fucn127(index, item);
			}
			$scope.fucn115 = function (index, item) {
				return $scope.fucn128(index, item);
			}
			$scope.fucn116 = function (index, item) {
				return $scope.fucn129(index, item);
			}
			$scope.fucn117 = function (index, item) {
				return $scope.fucn130(index, item);
			}
			$scope.fucn118 = function (index, item) {
				return $scope.fucn131(index, item);
			}
			$scope.fucn119 = function (index, item) {
				return $scope.fucn132(index, item);
			}
			$scope.fucn120 = function (index, item) {
				return $scope.fucn133(index, item);
			}
			$scope.fucn121 = function (index, item) {
				$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of five years, to a maximum of Rs. 10/20/40 lakhs for Micro/Small/Medium industries respectively</p>';
				if ($scope.user.plantInvest <= $scope.microEnterprises) {
					$scope.maxAmt = 0.10;
				}
				else if ($scope.user.plantInvest > $scope.microEnterprises && $scope.user.plantInvest <= $scope.smallEnterprises) {
					$scope.maxAmt = 0.20;
				}
				else if ($scope.user.plantInvest > $scope.smallEnterprises && $scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.maxAmt = 0.40;
				}
				$scope.eligibleLoanAmount = ($scope.user.totalInvestment * 60) / 100;
				$scope.eligibleAmount = ($scope.eligibleLoanAmount * 5) / 100;
				$scope.eligibleText = '';
				if ($scope.eligibleAmount < $scope.maxAmt) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.maxAmt - ($scope.eligibleAmount * (i + 1))) < $scope.eligibleAmount) {
							$scope.eligibleAmount = $scope.maxAmt - ($scope.eligibleAmount * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.eligibleAmount = $scope.maxAmt;
					$scope.eligibleText = " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn122 = function (index, item) {
				$scope.radioLblTxt = '<p>100% with respect to land allotted by the Government to IDCO or Govt/IDCO to Private Industrial Estate Developers.</p><p>100% when transfer of land/shed by Govt, IDCO and Private Industrial Estate developer to industrial units.</p>';
				if ($scope.districtVal == "A") {
					$scope.stampDutyVal = (1.3 * 5) / 100;
				}
				else {
					$scope.stampDutyVal = (0.3 * 5) / 100;
				}
				$scope.radioLblVal = 'of Rs.' + $scope.stampDutyVal.toFixed(2) + ' cr. per acres.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn123 = function (index, item) {
				$scope.radioLblTxt = '<p>Up to a contract demand of 5 MVA  for a period of 5 years</p><p>100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years</p>';
				if ($scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblVal = '5% of the energy charge exempted up to a contract demand of 5 MVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				else {
					$scope.radioLblVal = '8% of the energy charge exempted up to a contract demand of 5 MVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn124 = function (index, item) {
				$scope.radioLblTxt = '<p>100% for a period of 7 years, to a maximum of 200% of cost of plant and machinery </p>';
				$scope.eligibleText = "";
				$scope.turnover = ($scope.user.totalInvestment * 4);
				$scope.vatMaxVal = $scope.user.plantInvest * 2;
				$scope.vatVal = ($scope.turnover * 12.5) / 100;
				if ($scope.vatVal < $scope.vatMaxVal) {
					for (i = 0; i < 7; i++) {
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.vatMaxVal - ($scope.vatVal * (i + 1))) < $scope.vatVal) {
							$scope.vatVal = $scope.vatMaxVal - ($scope.vatVal * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.vatVal = $scope.vatMaxVal;
					$scope.eligibleText = " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn125 = function (index, item) {
				$scope.radioLblTxt = '<p>100% on acquisition of plant & machinery</p>';
				if ($scope.sectorVal == "IT/ITES" || $scope.sectorVal == "Tourism") {
					$scope.entryTax = 1;
				}
				else {
					$scope.entryTax = 2;
				}
				$scope.radioLblVal = 'of Rs. ' + (($scope.user.plantInvest * $scope.entryTax) / 100).toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn126 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.hideObj.push(item);
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn127 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of registration cost to a maximum of Rs. 10 lakhs</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn128 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of charges for a period of 3 years, to a maximum of Rs. 3 lakhs</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn129 = function (index, item) {
				$scope.radioLblTxt = '<p>100% reimbursement of cost for purchase up to Rs. 1 lakh for indigenous technology and Rs. 5 lakh for imported</p>';
				$scope.radioLblVal = '';
				// $scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn130 = function (index, item) {
				$scope.radioLblTxt = '<p>25% subsidy on cost of land</p><p>VAT Reimbursement for additional 2 years subject to the overall limit</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn131 = function (index, item) {
				$scope.radioLblTxt = '<p>50% of the infrastructure cost with a ceiling of Rs.10 crore per green field industrial park/cluster</p><p>50% of total cost with a ceiling of Rs.5 crore for up gradation of brown field clusters</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn132 = function (index, item) {
				$scope.radioLblTxt = '<p>Government shall provide exclusive sub-station for industrial park with energy requirement in excess of 20 MVA</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn133 = function (index, item) {
				$scope.radioLblTxt = '<p>100% up to 100 acres and 50% for balance area</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}

			/** phase 2 coding  **/
			$scope.fucn134 = function (index, item) {
				return $scope.fucn151(index, item);
			}
			$scope.fucn135 = function (index, item) {
				return $scope.fucn152(index, item);
			}
			$scope.fucn136 = function (index, item) {
				return $scope.fucn153(index, item);
			}
			$scope.fucn137 = function (index, item) {
				return $scope.fucn155(index, item);
			}
			$scope.fucn138 = function (index, item) {
				return $scope.fucn156(index, item);
			}
			$scope.fucn139 = function (index, item) {
				return $scope.fucn157(index, item);
			}
			$scope.fucn140 = function (index, item) {
				return $scope.fucn158(index, item);
			}
			$scope.fucn141 = function (index, item) {
				return $scope.fucn159(index, item);
			}
			$scope.fucn142 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.hideObj.push(item);
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn143 = function (index, item) {
				return $scope.fucn161(index, item);
			}
			$scope.fucn144 = function (index, item) {
				return $scope.fucn162(index, item);
			}
			$scope.fucn145 = function (index, item) {
				return $scope.fucn163(index, item);
			}
			$scope.fucn146 = function (index, item) {
				return $scope.fucn164(index, item);
			}
			$scope.fucn147 = function (index, item) {
				return $scope.fucn165(index, item);
			}
			$scope.fucn148 = function (index, item) {
				return $scope.fucn166(index, item);
			}
			$scope.fucn149 = function (index, item) {
				return $scope.fucn167(index, item);
			}
			$scope.fucn150 = function (index, item) {
				return $scope.fucn168(index, item);
			}
			$scope.fucn151 = function (index, item) {
				$scope.empMentVal = $scope.employmentIncentives();
				if ($scope.empMentVal[3] == undefined) {
					$scope.hideObj.push(item);
				}
				else {
					$scope.radioLblTxt = '<p>Rs. ' + $scope.empMentVal[3] + ' per unit reimbursed for 5 years.</p>';
				}
				$scope.yearlyAmt = ($scope.user.powerConsume * $scope.empMentVal[3]) * 12;
				$scope.totalAmt = $scope.yearlyAmt * 5;
				$scope.radioLblVal = 'Rs. ' + $scope.totalAmt.toFixed(2) + '/- for 5 years. @ Rs. ' + $scope.yearlyAmt.toFixed(2) + '/- per Annum.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn152 = function (index, item) {
				$scope.empMentVal = $scope.employmentIncentives();
				if ($scope.empMentVal[1] == undefined) {
					$scope.radioLblTxt = '';
					$scope.radioLblVal = '';
					$scope.hideObj.push(item);
				}
				else {
					$scope.radioLblTxt = '<p>Rs. ' + $scope.empMentVal[1] + ' per new employee trained And Rs. ' + $scope.empMentVal[2] + ' per employee skill upgradation</p>';
					$scope.empLen = 0;
					if ($scope.expectedEmployee == '20-40') {
						$scope.empLen = 30;
					}
					else if ($scope.expectedEmployee == '41-75') {

						$scope.empLen = 58;
					}
					else if ($scope.expectedEmployee == '76-80') {

						$scope.empLen = 78;
					}
					else if ($scope.expectedEmployee == '81-100') {

						$scope.empLen = 90;
					}
					else if ($scope.expectedEmployee == '101-150') {

						$scope.empLen = 125;
					}
					else if ($scope.expectedEmployee == '151-180') {

						$scope.empLen = 165;
					}
					else if ($scope.expectedEmployee == '181-200') {

						$scope.empLen = 190;
					}
					else if ($scope.expectedEmployee == '201-250') {

						$scope.empLen = 225;
					}
					else if ($scope.expectedEmployee == '251-300') {

						$scope.empLen = 275;
					}
					else if ($scope.expectedEmployee == '301-500') {

						$scope.empLen = 400;
					}
					else if ($scope.expectedEmployee == '501-750') {

						$scope.empLen = 625;
					}
					else if ($scope.expectedEmployee == '751-1000') {

						$scope.empLen = 875;
					}
					else if ($scope.expectedEmployee == '1001-1500') {

						$scope.empLen = 1250;
					}
					else if ($scope.expectedEmployee == '>1500') {

						$scope.empLen = 750;
					}
					$scope.newEmpLen = ($scope.empLen * 40) / 100;
					$scope.skillEmpLen = ($scope.empLen * 30) / 100;
					$scope.totalTrainingVal = ($scope.newEmpLen * $scope.empMentVal[1]) + ($scope.skillEmpLen * $scope.empMentVal[2])
					$scope.radioLblVal = 'of Rs.' + $scope.totalTrainingVal.toFixed(2) + '/-';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn153 = function (index, item) {
				$scope.empMentVal = $scope.employmentIncentives();
				if ($scope.empMentVal[0] == undefined) {
					$scope.radioLblTxt = '';
					$scope.hideObj.push(item);
				}
				else {
					$scope.radioLblTxt = '<p>Land provided @ 50% of market rate upto ' + $scope.empMentVal[0] + ' acres</p>';
				}
				if ($scope.districtVal == "A") {
					$scope.landVal = 1.3 * $scope.empMentVal[0];
				}
				else {
					$scope.landVal = 0.3 * $scope.empMentVal[0];
				}
				$scope.landInc = ($scope.landVal * 50) / 100;
				$scope.radioLblVal = 'of Rs.' + $scope.landInc.toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn154 = function (index, item) {
				$scope.empMentVal = $scope.employmentIncentives();
				if ($scope.empMentVal[4]) {
					$scope.radioLblTxt = $scope.empMentVal[4];
					$scope.radioLblVal = 'of Rs. ' + $scope.empMentVal[5] + ' cr.';
				}
				else {
					$scope.hideObj.push(item);
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn207 = function (index, item) {
				// if ($scope.sectorVal == "Healthcare") {
				// 	$scope.radioLblTxt = '<p>30% up to Rs. 10 crore for Grade 1 (minimum 50 beds).</p><p>Additional ceiling of Rs. 5 crore for higher grade.</p>';
				// 	if ($scope.user.totalBed <= 50) {
				// 		$scope.hideObj.push(item);
				// 	}
				// 	else {
				// 		$scope.bedCapital = ($scope.user.plantInvest * 30) / 100;
				// 		$scope.gradeVal = Math.floor($scope.user.totalBed / 100);
				// 		$scope.maxCap = 10;
				// 		if ($scope.gradeVal > 1) {
				// 			$scope.maxCap = 10;
				// 			$scope.maxCap = parseInt($scope.maxCap + (5 * Math.floor($scope.gradeVal - 1)));
				// 		}
				// 		if ($scope.bedCapital < $scope.maxCap) {
				// 			$scope.bedCapital = $scope.bedCapital;
				// 		}
				// 		else {
				// 			$scope.bedCapital = $scope.maxCap;
				// 		}
				// 		$scope.radioLblVal = " of Rs. " + $scope.bedCapital.toFixed(2) + " cr.";
				// 	}
				// 	return [$scope.radioLblTxt, $scope.radioLblVal];
				// }
				// else {
					$scope.empMentVal = $scope.employmentIncentives();
					if ($scope.empMentVal[4]) {
						$scope.radioLblTxt = $scope.empMentVal[4];
						$scope.radioLblVal = 'of Rs. ' + $scope.empMentVal[5] + ' cr.';
					}
					else {
						$scope.hideObj.push(item);
					}
					return [$scope.radioLblTxt, $scope.radioLblVal];
				// }
			}
			$scope.fucn208 = function (index, item) {
				return $scope.fucn207(index, item);
			}
			$scope.fucn155 = function (index, item) {
				$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of five years, to a maximum of Rs. 1 crore</p>';
				$scope.eligibleLoanAmount = ($scope.user.totalInvestment * 60) / 100;
				$scope.eligibleAmount = ($scope.eligibleLoanAmount * 5) / 100;
				$scope.eligibleText = '';
				if ($scope.eligibleAmount < 1) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if ((1 - ($scope.eligibleAmount * (i + 1))) < $scope.eligibleAmount) {
							$scope.eligibleAmount = 1 - ($scope.eligibleAmount * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.eligibleAmount = 1;
					$scope.eligibleText = " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "You are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn156 = function (index, item) {
				$scope.radioLblTxt = '<p>100% with respect to land allotted by the Government to IDCO or Govt/IDCO to Private Industrial Estate Developers</p><p>100% when transfer of land/shed by Govt, IDCO and Private Industrial Estate developer to industrial units</p>';
				if ($scope.districtVal == "A") {
					$scope.stampDutyVal = (1.3 * 5) / 100;
				}
				else {
					$scope.stampDutyVal = (0.3 * 5) / 100;
				}
				$scope.radioLblVal = 'of Rs.' + $scope.stampDutyVal.toFixed(2) + ' cr. per acres.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn157 = function (index, item) {
				$scope.radioLblTxt = '<p>Up to a contract demand of 5 MVA  for a period of 5 years.</p><p>100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years.</p>';
				if ($scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblVal = '5% of the energy charge exempted up to a contract demand of 5 MVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				else {
					$scope.radioLblVal = '8% of the energy charge exempted up to a contract demand of 5 MVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn158 = function (index, item) {
				$scope.radioLblTxt = '<p>100% for a period of 7 years, to a maximum of 200% of cost of plant and machinery.</p>';
				$scope.eligibleText = "";
				$scope.turnover = ($scope.user.totalInvestment * 4);
				$scope.vatMaxVal = $scope.user.plantInvest * 2;
				$scope.vatVal = ($scope.turnover * 12.5) / 100;
				if ($scope.vatVal < $scope.vatMaxVal) {
					for (i = 0; i < 7; i++) {
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.vatMaxVal - ($scope.vatVal * (i + 1))) < $scope.vatVal) {
							$scope.vatVal = $scope.vatMaxVal - ($scope.vatVal * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.vatVal = $scope.vatMaxVal;
					$scope.eligibleText = " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn159 = function (index, item) {
				$scope.radioLblTxt = '<p>100% on acquisition of plant & machinery.</p>';
				if ($scope.sectorVal == "IT/ITES" || $scope.sectorVal == "Tourism") {
					$scope.entryTax = 1;
				}
				else {
					$scope.entryTax = 2;
				}
				$scope.radioLblVal = 'of Rs. ' + (($scope.user.plantInvest * $scope.entryTax) / 100).toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn160 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.hideObj.push(item);
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn161 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of registration cost to a maximum of Rs. 10 lakhs</p>';
				if ($scope.sectorVal == "IT/ITES" || $scope.sectorVal == "Tourism") {
					$scope.entryTax = 1;
				}
				else {
					$scope.entryTax = 2;
				}
				$scope.radioLblVal = 'of Rs. ' + (($scope.user.plantInvest * $scope.entryTax) / 100).toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn162 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of charges for a period of 3 years, to a maximum of Rs. 3 lakhs</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn163 = function (index, item) {
				$scope.radioLblTxt = '<p>100% reimbursement of cost for purchase up to Rs. 1 lakh for indigenous technology and Rs. 5 lakh for imported</p>';
				$scope.radioLblVal = '';
				// $scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn164 = function (index, item) {
				$scope.radioLblTxt = '<p>25% subsidy on cost of land.</p><p>VAT Reimbursement for additional 2 years subject to the overall limit</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn165 = function (index, item) {
				$scope.radioLblTxt = '<p>10% of the land for large projects earmarked for Ancillary and Downstream park, up to 300 acres</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn166 = function (index, item) {
				$scope.radioLblTxt = '<p>50% of the infrastructure cost with a ceiling of Rs.10 crore per green field industrial park/cluster</p><p>50% of total cost with a ceiling of Rs.5 crore for up gradation of brown field clusters </p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn167 = function (index, item) {
				$scope.radioLblTxt = '<p>Government shall provide exclusive sub-station for industrial park with energy requirement in excess of 20 MVA </p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn168 = function (index, item) {
				$scope.radioLblTxt = '<p>100% up to 100 acres and 50% for balance area</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}


			/** phase 2 coding end here **/

			$scope.fucn169 = function (index, item) {
				return $scope.fucn185(index, item);
			}
			$scope.fucn170 = function (index, item) {
				return $scope.fucn186(index, item);
			}
			$scope.fucn171 = function (index, item) {
				return $scope.fucn187(index, item);
			}
			$scope.fucn172 = function (index, item) {
				$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of seven years, to a maximum of Rs. 1 crore</p>';
				$scope.eligibleLoanAmount = ($scope.user.totalInvestment * 60) / 100;
				$scope.eligibleAmount = ($scope.eligibleLoanAmount * 5) / 100;
				$scope.eligibleText = '';
				if ($scope.eligibleAmount < 1) {
					for (i = 0; i < 7; i++) {
						$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if ((1 - ($scope.eligibleAmount * (i + 1))) < $scope.eligibleAmount) {
							$scope.eligibleAmount = 1 - ($scope.eligibleAmount * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.eligibleAmount = 1;
					$scope.eligibleText = " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "You are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn173 = function (index, item) {
				return $scope.fucn189(index, item);
			}
			$scope.fucn174 = function (index, item) {
				return $scope.fucn190(index, item);
			}
			$scope.fucn175 = function (index, item) {
				return $scope.fucn191(index, item);
			}
			$scope.fucn176 = function (index, item) {
				return $scope.fucn192(index, item);
			}
			$scope.fucn177 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.hideObj.push(item);
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn178 = function (index, item) {
				return $scope.fucn194(index, item);
			}
			$scope.fucn179 = function (index, item) {
				return $scope.fucn195(index, item);
			}
			$scope.fucn180 = function (index, item) {
				return $scope.fucn196(index, item);
			}
			$scope.fucn181 = function (index, item) {
				return $scope.fucn197(index, item);
			}
			$scope.fucn182 = function (index, item) {
				return $scope.fucn198(index, item);
			}
			$scope.fucn183 = function (index, item) {
				return $scope.fucn199(index, item);
			}
			$scope.fucn184 = function (index, item) {
				return $scope.fucn200(index, item);
			}
			$scope.fucn185 = function (index, item) {
				$scope.empMentVal = $scope.employmentIncentives();
				if ($scope.empMentVal[3] == undefined) {
					$scope.hideObj.push(item);
				}
				else {
					$scope.radioLblTxt = '<p>Rs. ' + $scope.empMentVal[3] + ' per unit reimbursed for 5 years.</p>';
				}
				$scope.yearlyAmt = ($scope.user.powerConsume * $scope.empMentVal[3]) * 12;
				$scope.totalAmt = $scope.yearlyAmt * 5;
				$scope.radioLblVal = 'Rs. ' + $scope.totalAmt.toFixed(2) + '/- for 5 years. @ Rs. ' + $scope.yearlyAmt.toFixed(2) + '/- per Annum.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn186 = function (index, item) {
				$scope.empMentVal = $scope.employmentIncentives();
				if ($scope.empMentVal[1] == undefined) {
					$scope.radioLblTxt = '';
					$scope.radioLblVal = '';
					$scope.hideObj.push(item);
				}
				else {
					$scope.radioLblTxt = '<p>Rs. ' + $scope.empMentVal[1] + ' per new employee trained And Rs. ' + $scope.empMentVal[2] + ' per employee skill upgradation</p>';
					$scope.empLen = 0;
					if ($scope.expectedEmployee == '20-40') {
						$scope.empLen = 30;
					}
					else if ($scope.expectedEmployee == '41-75') {

						$scope.empLen = 58;
					}
					else if ($scope.expectedEmployee == '76-80') {

						$scope.empLen = 78;
					}
					else if ($scope.expectedEmployee == '81-100') {

						$scope.empLen = 90;
					}
					else if ($scope.expectedEmployee == '101-150') {

						$scope.empLen = 125;
					}
					else if ($scope.expectedEmployee == '151-180') {

						$scope.empLen = 165;
					}
					else if ($scope.expectedEmployee == '181-200') {

						$scope.empLen = 190;
					}
					else if ($scope.expectedEmployee == '201-250') {

						$scope.empLen = 225;
					}
					else if ($scope.expectedEmployee == '251-300') {

						$scope.empLen = 275;
					}
					else if ($scope.expectedEmployee == '301-500') {

						$scope.empLen = 400;
					}
					else if ($scope.expectedEmployee == '501-750') {

						$scope.empLen = 625;
					}
					else if ($scope.expectedEmployee == '751-1000') {

						$scope.empLen = 875;
					}
					else if ($scope.expectedEmployee == '1001-1500') {

						$scope.empLen = 1250;
					}
					else if ($scope.expectedEmployee == '>1500') {

						$scope.empLen = 750;
					}
					$scope.newEmpLen = ($scope.empLen * 40) / 100;
					$scope.skillEmpLen = ($scope.empLen * 30) / 100;
					$scope.totalTrainingVal = ($scope.newEmpLen * $scope.empMentVal[1]) + ($scope.skillEmpLen * $scope.empMentVal[2])
					$scope.radioLblVal = 'of Rs.' + $scope.totalTrainingVal.toFixed(2) + '/-';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn187 = function (index, item) {
				$scope.empMentVal = $scope.employmentIncentives();
				if ($scope.empMentVal[0] == undefined) {
					$scope.radioLblTxt = '';
					$scope.hideObj.push(item);
				}
				else {
					$scope.radioLblTxt = '<p>Land provided @ 50% of market rate upto ' + $scope.empMentVal[0] + ' acres</p>';
				}
				if ($scope.districtVal == "A") {
					$scope.landVal = 1.3 * $scope.empMentVal[0];
				}
				else {
					$scope.landVal = 0.3 * $scope.empMentVal[0];
				}
				$scope.landInc = ($scope.landVal * 50) / 100;
				$scope.radioLblVal = 'of Rs.' + $scope.landInc.toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn188 = function (index, item) {
				$scope.radioLblTxt = '<p>5%  per annum on term loan for a period of five years, to a maximum of Rs. 1 crore</p>';
				$scope.eligibleLoanAmount = ($scope.user.totalInvestment * 60) / 100;
				$scope.eligibleAmount = ($scope.eligibleLoanAmount * 5) / 100;
				$scope.eligibleText = '';
				if ($scope.eligibleAmount < 1) {
					for (i = 0; i < 5; i++) {
						$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if ((1 - ($scope.eligibleAmount * (i + 1))) < $scope.eligibleAmount) {
							$scope.eligibleAmount = 1 - ($scope.eligibleAmount * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.eligibleAmount = 1;
					$scope.eligibleText = " Rs. " + $scope.eligibleAmount.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "You are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn189 = function (index, item) {
				$scope.radioLblTxt = '<p>100% with respect to land allotted by the Government to IDCO or Govt/IDCO to Private Industrial Estate Developers</p><p>100% when transfer of land/shed by Govt, IDCO and Private Industrial Estate developer to industrial units</p>';
				if ($scope.districtVal == "A") {
					$scope.stampDutyVal = (1.3 * 5) / 100;
				}
				else {
					$scope.stampDutyVal = (0.3 * 5) / 100;
				}
				$scope.radioLblVal = 'of Rs.' + $scope.stampDutyVal.toFixed(2) + ' cr. per acres.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn190 = function (index, item) {
				$scope.radioLblTxt = '<p>Up to a contract demand of 5 MVA  for a period of 5 years</p><p>100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years</p>';
				if ($scope.user.plantInvest <= $scope.mediumEnterprises) {
					$scope.radioLblVal = '5% of the energy charge exempted up to a contract demand of 5 MVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				else {
					$scope.radioLblVal = '8% of the energy charge exempted up to a contract demand of 5 MVA  for a period of 5 years.100% for Industrial units setting up Captive Power Plant with non-conventional sources & bio-fuel, for a period of 5 years';
				}
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn191 = function (index, item) {
				// $scope.radioLblTxt = '<p>100% reimbursement for a period of 5 years limited to 100% of the fixed capital investment.</p>';
				$scope.radioLblTxt = '<p>100% for a period of 7 years, to a maximum of 200% of cost of plant and machinery </p>';
				$scope.eligibleText = "";
				$scope.turnover = ($scope.user.totalInvestment * 4);
				$scope.vatMaxVal = $scope.user.plantInvest * 2;
				$scope.vatVal = ($scope.turnover * 12.5) / 100;
				if ($scope.vatVal < $scope.vatMaxVal) {
					for (i = 0; i < 7; i++) {
						$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 1) + ",";
						if (($scope.vatMaxVal - ($scope.vatVal * (i + 1))) < $scope.vatVal) {
							$scope.vatVal = $scope.vatMaxVal - ($scope.vatVal * (i + 1));
							$scope.eligibleText += " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year " + (i + 2) + ".";
							break;
						}
					}
				}
				else {
					$scope.vatVal = $scope.vatMaxVal;
					$scope.eligibleText = " Rs. " + $scope.vatVal.toFixed(2) + " cr. for year 1.";
				}
				$scope.radioLblVal = "you are eligible of" + $scope.eligibleText;
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn192 = function (index, item) {
				$scope.radioLblTxt = '<p>100% on acquisition of plant & machinery</p>';
				if ($scope.sectorVal == "IT/ITES" || $scope.sectorVal == "Tourism") {
					$scope.entryTax = 1;
				}
				else {
					$scope.entryTax = 2;
				}
				$scope.radioLblVal = 'of Rs. ' + (($scope.user.plantInvest * $scope.entryTax) / 100).toFixed(2) + ' cr.';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn193 = function (index, item) {
				$scope.radioLblTxt = '';
				$scope.hideObj.push(item);
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn194 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of registration cost to a maximum of Rs. 10 lakhs</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn195 = function (index, item) {
				$scope.radioLblTxt = '<p>100% of charges for a period of 3 years, to a maximum of Rs. 3 lakhs.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn196 = function (index, item) {
				$scope.radioLblTxt = '<p>100% reimbursement of cost for purchase up to Rs. 1 lakh for indigenous technology and Rs. 5 lakh for imported</p>';
				$scope.radioLblVal = '';
				// $scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn197 = function (index, item) {
				$scope.radioLblTxt = '<p>25% subsidy on cost of land.</p><p>VAT Reimbursement for additional 2 years subject to the overall limit</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn198 = function (index, item) {
				$scope.radioLblTxt = '<p>50% of the infrastructure cost with a ceiling of Rs.10 crore per green field industrial park/cluster</p><p>50% of total cost with a ceiling of Rs.5 crore for up gradation of brown field clusters</p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn199 = function (index, item) {
				$scope.radioLblTxt = '<p>Government shall provide exclusive sub-station for industrial park with energy requirement in excess of 20 MVA </p>';
				$scope.radioLblVal = '';
				$scope.hideObj.push(item);
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn200 = function (index, item) {
				$scope.radioLblTxt = '<p>100% up to 100 acres and 50% for balance area</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn202 = function (index, item) {
				$scope.radioLblTxt = '<p>Exemption on machinery and equipment for a period of 3 years from registration</p>';
				$scope.entryTax = 2;
				$scope.radioLblVal = 'of Rs. ' + (($scope.user.plantInvest * $scope.entryTax) / 100).toFixed(2) + ' cr. for a period of 3 years from registration';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}
			$scope.fucn203 = function (index, item) {
				$scope.radioLblTxt = '<p>Upto USD 13 per person per month to be reimbursed.</p><p>Reimbursement of expenditure towards ESI and EPF subject to maximum of USD 0.33 Million.</p>';
				$scope.radioLblVal = '';
				return [$scope.radioLblTxt, $scope.radioLblVal];
			}

			$scope.subsidiesForm = false;
			$scope.subsidiesEligible = true;
			$scope.subsidiesResult = false;

		}

		/*********  END HERE: Incentive form button click function ********/

		/*********  START HERE: Code for remove the panel box if it has no policy support ********/

		$scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
			$('.incentivesList').each(function () {
				if(!$(this).find('.list-group-item').length){
					$(this).remove();
				}
			});
		});
		
		/*********  END HERE: Code for remove the panel box if it has no policy support ********/


		/***** Function for select incentives on radio label click *****/

		$(document).on('change', '.incentiveCheckbox', function () {
			if ($(this).is(":checked")) {
				$(this).parents('.panel').find('.list-group:first-child input[type="radio"]').prop("checked", true);
			}
			else {
				$(this).parents('.panel').find('.list-group:first-child input[type="radio"]').prop("checked", false);
			}
		});
		$(document).on('change', '.incentiveRadiobtn', function () {
			if ($(this).is(":checked")) {
				$(this).parents('.panel').find('.incentiveCheckbox').prop("checked", true);
			}
		});

		/***** END HERE: Function for create array of selected incentives *****/

		/*********  START HERE: Subsidies Next button click function ********/

		$scope.subsidiesBack = function () {
			$scope.subsidiesForm = true;
			$scope.subsidiesEligible = false;
			location.reload();
		}
		$scope.subsidiesNext = function () {

			/***** Function for create array of selected incentives *****/
			$scope.selectedIncObj = [];
			$('.incentivesList').each(function () {
				if ($(this).find('input[type="checkbox"]').is(':checked')) {
					$scope.inctvVal = $(this).find('.panel-heading label').text();
					$scope.policyNm = $(this).find('input[type="radio"]:checked').next('.policyNm').text();
					$scope.value = $(this).find('input[type="radio"]:checked').parent().find('.valueText').text();
					if ($scope.value == '') {
						$scope.value = $(this).find('input[type="radio"]:checked').parent().find('.plicyTxt').text();
					}
					$scope.selectedIncObj.push({ 'incentive': $scope.inctvVal, 'policy': $scope.policyNm, 'value': $scope.value });
				}
			});
			/***** END HERE: Function for create array of selected incentives *****/

			/*$scope.capitalSubsidyVal = ($scope.user.plantInvest * 33)/100;
			$scope.interestSubsidyVal = ($scope.user.plantInvest * 5)/100;*/
			if ($scope.selectedIncObj.length) {
				// var userObj = $scope.user;
				// var indusNameobj = userObj.industryNm;
				// var sectorobj = userObj.sectorNm;
				// var sectNameObj = sectorobj.text;
				// var subSectorobj = userObj.enterpriseType;
				// var subSectNmObj = subSectorobj.text;//alert(subSectNmObj);
				// var powerobj = userObj.powerConsume;
				// var investamtobj = userObj.totalInvestment;
				// var plantobj = userObj.plantInvest;
				// var locobj = userObj.district;
				// var distObj = locobj.text;
				// var localEmpobj = userObj.expectedEmployee;
				// var locEmpNmobj = localEmpobj.text;
				// var investCatobj = userObj.investorCategory;
				// var genderobj = userObj.gender;
				// var bedobj = userObj.totalBed;

				// var incentivQry = '';
				// for (j = 0; j < $scope.selectedIncObj.length; j++) {
				// 	var incentData = $scope.selectedIncObj[j];
				// 	var incentObj = incentData.incentive;
				// 	var policyObj = incentData.policy;
				// 	var valueObj = incentData.value;
				// 	incentivQry += '(@INCENT_CAL,"' + incentObj + '","' + policyObj + '","' + valueObj + '"),';
				// }

				// incentivQry = incentivQry.substring(0, incentivQry.length - 1);
				// addIncentivecal('A', indusNameobj, sectNameObj, subSectNmObj, powerobj, investamtobj, plantobj, distObj, locEmpNmobj, investCatobj, genderobj, bedobj, $scope, incentivQry);

				// setTimeout(function () {
				// 	$('.refNumDiv').html('<img src="'+$scope.domainName+'includes/barcode.php?text= Reference No : ' + $scope.refNum + '" style="float: none;margin: 0px;"/>');
				// }, 500);

				$scope.subsidiesForm = false;
				$scope.subsidiesEligible = false;
				$scope.subsidiesResult = true;
			}
			else {
				alert('You have not selected any incentives.');
				return false;
			}



		}

		/*********  END HERE: Subsidies Next button click function ********/

		$scope.printRes = function (printSectionId) {
			var innerContents = document.getElementById(printSectionId).innerHTML;
			var popupWinindow = window.open('', '_blank', 'width=800,height=700,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
			popupWinindow.document.open();
			popupWinindow.document.write('<html><head><style>.fltlft {float: left;}.fltrht {float: right;}.clear {clear: both;}</style></head><body onload="window.print()">' + innerContents + '</html>');
			popupWinindow.document.close();
		}

		// exit from Incentive Calculator page
		$scope.exitInfowizard = function () {
			var res = confirm('Are you sure you want to exit from Incentive Calculator');
			if (res == true) {
			    location.href = $scope.domainName + 'Incentives/IncentiveCalc.aspx';
			} else {

			}

		}

	})