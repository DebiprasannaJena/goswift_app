/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/
CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    //Code Added By Dilip Kumar Tripathy on dated 14-May-2013
    //Purpose : To solve the image upload issue
    config.filebrowserBrowseUrl = 'https://localhost/SWP_AMS/Portal/CMS/CMSImages.aspx';
    config.allowedContent = true;
    config.filebrowserWindowWidth = 500;
    config.filebrowserWindowHeight = 500;
    config.filebrowserUploadUrl = '../../UploadFile.ashx';
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordRemoveStyles = false;
    config.removePlugins = 'liststyle,tabletools,contextmenu';
    config.disableNativeSpellChecker = false;
//    config.extraPlugins = 'sourcedialog';
//    config.removePlugins = 'sourcearea';
    /*Added by Dilip Kumar Tripathy on dated 8-Mar-2013 
    Purpose : To disable ckeditor menu
    */

    //  document=  'Save','NewPage','Preview','Print',
    //CLIPBOARD='Cut','PasteFromWord',
    //basicstyles= 'Subscript', 'Superscript','Strike',
    //  PARAGRAPH == 'BidiLtr', 'BidiRtl'
    //TOOLS='Maximize', 'ShowBlocks',


    config.toolbar = 'MyToolbar';

    config.toolbar_MyToolbar =
	[
	    { name: 'document', items: [ 'Source', 'DocProps',   '-', 'Templates'] },
	    { name: 'clipboard', items: [ 'Copy', 'Paste', 'PasteText',  '-', 'Undo', 'Redo'] },
	    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline',   '-', 'RemoveFormat'] },
	    { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-',
	    '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', ]
	    },
//	    { name: 'links', items: ['Link', 'Unlink'] },
	    { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar'] },
	    { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
	    { name: 'colors', items: ['TextColor', 'BGColor'] },
	    { name: 'tools', items: [ '-', 'About'] }
	];

};