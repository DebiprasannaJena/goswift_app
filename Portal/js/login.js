$('.form').find('input').on('keyup blur focus keypress', function (e) {
  
  var $this = $(this),
      label = $this.prev('label');
 if (e.type === 'keypress') {
		label.addClass('active highlight');
          label.removeClass('highlight');
        
    }else if (e.type === 'blur') {
    	if( $this.val() === '' ) {
    		label.removeClass('active highlight'); 
			} else {
		    label.removeClass('highlight');   
			}   
    } else if (e.type === 'focus') {
      
     
	  label.removeClass('highlight'); 
    		
			
			
    }

});
