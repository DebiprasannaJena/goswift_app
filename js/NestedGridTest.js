
/// <reference path="jquery-1.4.1.min.js" />
function Genaratexml() {
   
    var $root = $("<Root>");
    var $xml = $("<DocumentElement>");
    var cnt = 0;
    $("[id*=tblSubGrid] >tbody > tr").each(function () {
        cnt = cnt + 1;
        var $schedule = $("<OrderTable>");
        var $row = $(this);
        var $Item = $("<Item>");
        var parentId = $row.parent().parent().parent().parent().parent().prev('tr').find('td').eq(2).find('[name*=MainVal]').val();
        $Item.attr('id', parentId);
        var itemDesc = $row.find('td').eq(1).find('[name*=SubText]').val();
        $Item.text(itemDesc);
        var $Price = $("<Price>");
        $Price.text('2');
        var $Quantity = $("<Quantity>");
        $Quantity.text($row.find('td').eq(2).find('[name*=SubNumber]').val());

        //alert($row.find('td').eq(0).text());


        $schedule.append($Item);
        $schedule.append($Price);
        $schedule.append($Quantity);

        $xml.append($schedule);
        $root.append($xml);


    });
   
    return $root.html();

}