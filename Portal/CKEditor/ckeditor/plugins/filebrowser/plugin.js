  1 ﻿/*
  2 Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
  3 For licensing, see LICENSE.html or http://ckeditor.com/license
  4 */
  5 
  6 /**
  7  * @fileOverview The "filebrowser" plugin that adds support for file uploads and
  8  *               browsing.
  9  *
 10  * When a file is uploaded or selected inside the file browser, its URL is
 11  * inserted automatically into a field defined in the <code>filebrowser</code>
 12  * attribute. In order to specify a field that should be updated, pass the tab ID and
 13  * the element ID, separated with a colon.<br /><br />
 14  *
 15  * <strong>Example 1: (Browse)</strong>
 16  *
 17  * <pre>
 18  * {
 19  * 	type : 'button',
 20  * 	id : 'browse',
 21  * 	filebrowser : 'tabId:elementId',
 22  * 	label : editor.lang.common.browseServer
 23  * }
 24  * </pre>
 25  *
 26  * If you set the <code>filebrowser</code> attribute for an element other than
 27  * the <code>fileButton</code>, the <code>Browse</code> action will be triggered.<br /><br />
 28  *
 29  * <strong>Example 2: (Quick Upload)</strong>
 30  *
 31  * <pre>
 32  * {
 33  * 	type : 'fileButton',
 34  * 	id : 'uploadButton',
 35  * 	filebrowser : 'tabId:elementId',
 36  * 	label : editor.lang.common.uploadSubmit,
 37  * 	'for' : [ 'upload', 'upload' ]
 38  * }
 39  * </pre>
 40  *
 41  * If you set the <code>filebrowser</code> attribute for a <code>fileButton</code>
 42  * element, the <code>QuickUpload</code> action will be executed.<br /><br />
 43  *
 44  * The filebrowser plugin also supports more advanced configuration performed through
 45  * a JavaScript object.
 46  *
 47  * The following settings are supported:
 48  *
 49  * <ul>
 50  * <li><code>action</code> – <code>Browse</code> or <code>QuickUpload</code>.</li>
 51  * <li><code>target</code> – the field to update in the <code><em>tabId:elementId</em></code> format.</li>
 52  * <li><code>params</code> – additional arguments to be passed to the server connector (optional).</li>
 53  * <li><code>onSelect</code> – a function to execute when the file is selected/uploaded (optional).</li>
 54  * <li><code>url</code> – the URL to be called (optional).</li>
 55  * </ul>
 56  *
 57  * <strong>Example 3: (Quick Upload)</strong>
 58  *
 59  * <pre>
 60  * {
 61  * 	type : 'fileButton',
 62  * 	label : editor.lang.common.uploadSubmit,
 63  * 	id : 'buttonId',
 64  * 	filebrowser :
 65  * 	{
 66  * 		action : 'QuickUpload', // required
 67  * 		target : 'tab1:elementId', // required
 68  * 		params : // optional
 69  * 		{
 70  * 			type : 'Files',
 71  * 			currentFolder : '/folder/'
 72  * 		},
 73  * 		onSelect : function( fileUrl, errorMessage ) // optional
 74  * 		{
 75  * 			// Do not call the built-in selectFuntion.
 76  * 			// return false;
 77  * 		}
 78  * 	},
 79  * 	'for' : [ 'tab1', 'myFile' ]
 80  * }
 81  * </pre>
 82  *
 83  * Suppose you have a file element with an ID of <code>myFile</code>, a text
 84  * field with an ID of <code>elementId</code> and a <code>fileButton</code>.
 85  * If the <code>filebowser.url</code> attribute is not specified explicitly,
 86  * the form action will be set to <code>filebrowser[<em>DialogWindowName</em>]UploadUrl</code>
 87  * or, if not specified, to <code>filebrowserUploadUrl</code>. Additional parameters
 88  * from the <code>params</code> object will be added to the query string. It is
 89  * possible to create your own <code>uploadHandler</code> and cancel the built-in
 90  * <code>updateTargetElement</code> command.<br /><br />
 91  *
 92  * <strong>Example 4: (Browse)</strong>
 93  *
 94  * <pre>
 95  * {
 96  * 	type : 'button',
 97  * 	id : 'buttonId',
 98  * 	label : editor.lang.common.browseServer,
 99  * 	filebrowser :
100  * 	{
101  * 		action : 'Browse',
102  * 		url : '/ckfinder/ckfinder.html&type=Images',
103  * 		target : 'tab1:elementId'
104  * 	}
105  * }
106  * </pre>
107  *
108  * In this example, when the button is pressed, the file browser will be opened in a
109  * popup window. If you do not specify the <code>filebrowser.url</code> attribute,
110  * <code>filebrowser[<em>DialogName</em>]BrowseUrl</code> or
111  * <code>filebrowserBrowseUrl</code> will be used. After selecting a file in the file
112  * browser, an element with an ID of <code>elementId</code> will be updated. Just
113  * like in the third example, a custom <code>onSelect</code> function may be defined.
114  */
115 ( function()
116 {
117 	/*
118 	 * Adds (additional) arguments to given url.
119 	 *
120 	 * @param {String}
121 	 *            url The url.
122 	 * @param {Object}
123 	 *            params Additional parameters.
124 	 */
125 	function addQueryString( url, params )
126 	{
127 		var queryString = [];
128 
129 		if ( !params )
130 			return url;
131 		else
132 		{
133 			for ( var i in params )
134 				queryString.push( i + "=" + encodeURIComponent( params[ i ] ) );
135 		}
136 
137 		return url + ( ( url.indexOf( "?" ) != -1 ) ? "&" : "?" ) + queryString.join( "&" );
138 	}
139 
140 	/*
141 	 * Make a string's first character uppercase.
142 	 *
143 	 * @param {String}
144 	 *            str String.
145 	 */
146 	function ucFirst( str )
147 	{
148 		str += '';
149 		var f = str.charAt( 0 ).toUpperCase();
150 		return f + str.substr( 1 );
151 	}
152 
153 	/*
154 	 * The onlick function assigned to the 'Browse Server' button. Opens the
155 	 * file browser and updates target field when file is selected.
156 	 *
157 	 * @param {CKEDITOR.event}
158 	 *            evt The event object.
159 	 */
160 	function browseServer( evt )
161 	{
162 		var dialog = this.getDialog();
163 		var editor = dialog.getParentEditor();
164 
165 		editor._.filebrowserSe = this;
166 
167 		var width = editor.config[ 'filebrowser' + ucFirst( dialog.getName() ) + 'WindowWidth' ]
168 				|| editor.config.filebrowserWindowWidth || '80%';
169 		var height = editor.config[ 'filebrowser' + ucFirst( dialog.getName() ) + 'WindowHeight' ]
170 				|| editor.config.filebrowserWindowHeight || '70%';
171 
172 		var params = this.filebrowser.params || {};
173 		params.CKEditor = editor.name;
174 		params.CKEditorFuncNum = editor._.filebrowserFn;
175 		if ( !params.langCode )
176 			params.langCode = editor.langCode;
177 
178 		var url = addQueryString( this.filebrowser.url, params );
179 		// TODO: V4: Remove backward compatibility (#8163).
180 		editor.popup( url, width, height, editor.config.filebrowserWindowFeatures || editor.config.fileBrowserWindowFeatures );
181 	}
182 
183 	/*
184 	 * The onlick function assigned to the 'Upload' button. Makes the final
185 	 * decision whether form is really submitted and updates target field when
186 	 * file is uploaded.
187 	 *
188 	 * @param {CKEDITOR.event}
189 	 *            evt The event object.
190 	 */
191 	function uploadFile( evt )
192 	{
193 		var dialog = this.getDialog();
194 		var editor = dialog.getParentEditor();
195 
196 		editor._.filebrowserSe = this;
197 
198 		// If user didn't select the file, stop the upload.
199 		if ( !dialog.getContentElement( this[ 'for' ][ 0 ], this[ 'for' ][ 1 ] ).getInputElement().$.value )
200 			return false;
201 
202 		if ( !dialog.getContentElement( this[ 'for' ][ 0 ], this[ 'for' ][ 1 ] ).getAction() )
203 			return false;
204 
205 		return true;
206 	}
207 
208 	/*
209 	 * Setups the file element.
210 	 *
211 	 * @param {CKEDITOR.ui.dialog.file}
212 	 *            fileInput The file element used during file upload.
213 	 * @param {Object}
214 	 *            filebrowser Object containing filebrowser settings assigned to
215 	 *            the fileButton associated with this file element.
216 	 */
217 	function setupFileElement( editor, fileInput, filebrowser )
218 	{
219 		var params = filebrowser.params || {};
220 		params.CKEditor = editor.name;
221 		params.CKEditorFuncNum = editor._.filebrowserFn;
222 		if ( !params.langCode )
223 			params.langCode = editor.langCode;
224 
225 		fileInput.action = addQueryString( filebrowser.url, params );
226 		fileInput.filebrowser = filebrowser;
227 	}
228 
229 	/*
230 	 * Traverse through the content definition and attach filebrowser to
231 	 * elements with 'filebrowser' attribute.
232 	 *
233 	 * @param String
234 	 *            dialogName Dialog name.
235 	 * @param {CKEDITOR.dialog.definitionObject}
236 	 *            definition Dialog definition.
237 	 * @param {Array}
238 	 *            elements Array of {@link CKEDITOR.dialog.definition.content}
239 	 *            objects.
240 	 */
241 	function attachFileBrowser( editor, dialogName, definition, elements )
242 	{
243 		var element, fileInput;
244 
245 		for ( var i in elements )
246 		{
247 			element = elements[ i ];
248 
249 			if ( element.type == 'hbox' || element.type == 'vbox' || element.type == 'fieldset' )
250 				attachFileBrowser( editor, dialogName, definition, element.children );
251 
252 			if ( !element.filebrowser )
253 				continue;
254 
255 			if ( typeof element.filebrowser == 'string' )
256 			{
257 				var fb =
258 				{
259 					action : ( element.type == 'fileButton' ) ? 'QuickUpload' : 'Browse',
260 					target : element.filebrowser
261 				};
262 				element.filebrowser = fb;
263 			}
264 
265 			if ( element.filebrowser.action == 'Browse' )
266 			{
267 				var url = element.filebrowser.url;
268 				if ( url === undefined )
269 				{
270 					url = editor.config[ 'filebrowser' + ucFirst( dialogName ) + 'BrowseUrl' ];
271 					if ( url === undefined )
272 						url = editor.config.filebrowserBrowseUrl;
273 				}
274 
275 				if ( url )
276 				{
277 					element.onClick = browseServer;
278 					element.filebrowser.url = url;
279 					element.hidden = false;
280 				}
281 			}
282 			else if ( element.filebrowser.action == 'QuickUpload' && element[ 'for' ] )
283 			{
284 				url = element.filebrowser.url;
285 				if ( url === undefined )
286 				{
287 					url = editor.config[ 'filebrowser' + ucFirst( dialogName ) + 'UploadUrl' ];
288 					if ( url === undefined )
289 						url = editor.config.filebrowserUploadUrl;
290 				}
291 
292 				if ( url )
293 				{
294 					var onClick = element.onClick;
295 					element.onClick = function( evt )
296 					{
297 						// "element" here means the definition object, so we need to find the correct
298 						// button to scope the event call
299 						var sender = evt.sender;
300 						if ( onClick && onClick.call( sender, evt ) === false )
301 							return false;
302 
303 						return uploadFile.call( sender, evt );
304 					};
305 
306 					element.filebrowser.url = url;
307 					element.hidden = false;
308 					setupFileElement( editor, definition.getContents( element[ 'for' ][ 0 ] ).get( element[ 'for' ][ 1 ] ), element.filebrowser );
309 				}
310 			}
311 		}
312 	}
313 
314 	/*
315 	 * Updates the target element with the url of uploaded/selected file.
316 	 *
317 	 * @param {String}
318 	 *            url The url of a file.
319 	 */
320 	function updateTargetElement( url, sourceElement )
321 	{
322 		var dialog = sourceElement.getDialog();
323 		var targetElement = sourceElement.filebrowser.target || null;
324 
325 		// If there is a reference to targetElement, update it.
326 		if ( targetElement )
327 		{
328 			var target = targetElement.split( ':' );
329 			var element = dialog.getContentElement( target[ 0 ], target[ 1 ] );
330 			if ( element )
331 			{
332 				element.setValue( url );
333 				dialog.selectPage( target[ 0 ] );
334 			}
335 		}
336 	}
337 
338 	/*
339 	 * Returns true if filebrowser is configured in one of the elements.
340 	 *
341 	 * @param {CKEDITOR.dialog.definitionObject}
342 	 *            definition Dialog definition.
343 	 * @param String
344 	 *            tabId The tab id where element(s) can be found.
345 	 * @param String
346 	 *            elementId The element id (or ids, separated with a semicolon) to check.
347 	 */
348 	function isConfigured( definition, tabId, elementId )
349 	{
350 		if ( elementId.indexOf( ";" ) !== -1 )
351 		{
352 			var ids = elementId.split( ";" );
353 			for ( var i = 0 ; i < ids.length ; i++ )
354 			{
355 				if ( isConfigured( definition, tabId, ids[i] ) )
356 					return true;
357 			}
358 			return false;
359 		}
360 
361 		var elementFileBrowser = definition.getContents( tabId ).get( elementId ).filebrowser;
362 		return ( elementFileBrowser && elementFileBrowser.url );
363 	}
364 
365 	function setUrl( fileUrl, data )
366 	{
367 		var dialog = this._.filebrowserSe.getDialog(),
368 			targetInput = this._.filebrowserSe[ 'for' ],
369 			onSelect = this._.filebrowserSe.filebrowser.onSelect;
370 
371 		if ( targetInput )
372 			dialog.getContentElement( targetInput[ 0 ], targetInput[ 1 ] ).reset();
373 
374 		if ( typeof data == 'function' && data.call( this._.filebrowserSe ) === false )
375 			return;
376 
377 		if ( onSelect && onSelect.call( this._.filebrowserSe, fileUrl, data ) === false )
378 			return;
379 
380 		// The "data" argument may be used to pass the error message to the editor.
381 		if ( typeof data == 'string' && data )
382 			alert( data );
383 
384 		if ( fileUrl )
385 			updateTargetElement( fileUrl, this._.filebrowserSe );
386 	}
387 
388 	CKEDITOR.plugins.add( 'filebrowser',
389 	{
390 		init : function( editor, pluginPath )
391 		{
392 			editor._.filebrowserFn = CKEDITOR.tools.addFunction( setUrl, editor );
393 			editor.on( 'destroy', function () { CKEDITOR.tools.removeFunction( this._.filebrowserFn ); } );
394 		}
395 	} );
396 
397 	CKEDITOR.on( 'dialogDefinition', function( evt )
398 	{
399 		var definition = evt.data.definition,
400 			element;
401 		// Associate filebrowser to elements with 'filebrowser' attribute.
402 		for ( var i in definition.contents )
403 		{
404 			if ( ( element = definition.contents[ i ] ) )
405 			{
406 				attachFileBrowser( evt.editor, evt.data.name, definition, element.elements );
407 				if ( element.hidden && element.filebrowser )
408 				{
409 					element.hidden = !isConfigured( definition, element[ 'id' ], element.filebrowser );
410 				}
411 			}
412 		}
413 	} );
414 
415 } )();
416 
417 /**
418  * The location of an external file browser that should be launched when the <strong>Browse Server</strong>
419  * button is pressed. If configured, the <strong>Browse Server</strong> button will appear in the
420  * <strong>Link</strong>, <strong>Image</strong>, and <strong>Flash</strong> dialog windows.
421  * @see The <a href="http://docs.cksource.com/CKEditor_3.x/Developers_Guide/File_Browser_(Uploader)">File Browser/Uploader</a> documentation.
422  * @name CKEDITOR.config.filebrowserBrowseUrl
423  * @since 3.0
424  * @type String
425  * @default <code>''</code> (empty string = disabled)
426  * @example
427  * config.filebrowserBrowseUrl = '/browser/browse.php';
428  */
429 
430 /**
431  * The location of the script that handles file uploads.
432  * If set, the <strong>Upload</strong> tab will appear in the <strong>Link</strong>, <strong>Image</strong>,
433  * and <strong>Flash</strong> dialog windows.
434  * @name CKEDITOR.config.filebrowserUploadUrl
435  * @see The <a href="http://docs.cksource.com/CKEditor_3.x/Developers_Guide/File_Browser_(Uploader)">File Browser/Uploader</a> documentation.
436  * @since 3.0
437  * @type String
438  * @default <code>''</code> (empty string = disabled)
439  * @example
440  * config.filebrowserUploadUrl = '/uploader/upload.php';
441  */
442 
443 /**
444  * The location of an external file browser that should be launched when the <strong>Browse Server</strong>
445  * button is pressed in the <strong>Image</strong> dialog window.
446  * If not set, CKEditor will use <code>{@link CKEDITOR.config.filebrowserBrowseUrl}</code>.
447  * @name CKEDITOR.config.filebrowserImageBrowseUrl
448  * @since 3.0
449  * @type String
450  * @default <code>''</code> (empty string = disabled)
451  * @example
452  * config.filebrowserImageBrowseUrl = '/browser/browse.php?type=Images';
453  */
454 
455 /**
456  * The location of an external file browser that should be launched when the <strong>Browse Server</strong>
457  * button is pressed in the <strong>Flash</strong> dialog window.
458  * If not set, CKEditor will use <code>{@link CKEDITOR.config.filebrowserBrowseUrl}</code>.
459  * @name CKEDITOR.config.filebrowserFlashBrowseUrl
460  * @since 3.0
461  * @type String
462  * @default <code>''</code> (empty string = disabled)
463  * @example
464  * config.filebrowserFlashBrowseUrl = '/browser/browse.php?type=Flash';
465  */
466 
467 /**
468  * The location of the script that handles file uploads in the <strong>Image</strong> dialog window.
469  * If not set, CKEditor will use <code>{@link CKEDITOR.config.filebrowserUploadUrl}</code>.
470  * @name CKEDITOR.config.filebrowserImageUploadUrl
471  * @since 3.0
472  * @type String
473  * @default <code>''</code> (empty string = disabled)
474  * @example
475  * config.filebrowserImageUploadUrl = '/uploader/upload.php?type=Images';
476  */
477 
478 /**
479  * The location of the script that handles file uploads in the <strong>Flash</strong> dialog window.
480  * If not set, CKEditor will use <code>{@link CKEDITOR.config.filebrowserUploadUrl}</code>.
481  * @name CKEDITOR.config.filebrowserFlashUploadUrl
482  * @since 3.0
483  * @type String
484  * @default <code>''</code> (empty string = disabled)
485  * @example
486  * config.filebrowserFlashUploadUrl = '/uploader/upload.php?type=Flash';
487  */
488 
489 /**
490  * The location of an external file browser that should be launched when the <strong>Browse Server</strong>
491  * button is pressed in the <strong>Link</strong> tab of the <strong>Image</strong> dialog window.
492  * If not set, CKEditor will use <code>{@link CKEDITOR.config.filebrowserBrowseUrl}</code>.
493  * @name CKEDITOR.config.filebrowserImageBrowseLinkUrl
494  * @since 3.2
495  * @type String
496  * @default <code>''</code> (empty string = disabled)
497  * @example
498  * config.filebrowserImageBrowseLinkUrl = '/browser/browse.php';
499  */
500 
501 /**
502  * The features to use in the file browser popup window.
503  * @name CKEDITOR.config.filebrowserWindowFeatures
504  * @since 3.4.1
505  * @type String
506  * @default <code>'location=no,menubar=no,toolbar=no,dependent=yes,minimizable=no,modal=yes,alwaysRaised=yes,resizable=yes,scrollbars=yes'</code>
507  * @example
508  * config.filebrowserWindowFeatures = 'resizable=yes,scrollbars=no';
509  */
510 
511 /**
512  * The width of the file browser popup window. It can be a number denoting a value in
513  * pixels or a percent string.
514  * @name CKEDITOR.config.filebrowserWindowWidth
515  * @type Number|String
516  * @default <code>'80%'</code>
517  * @example
518  * config.filebrowserWindowWidth = 750;
519  * @example
520  * config.filebrowserWindowWidth = '50%';
521  */
522 
523 /**
524  * The height of the file browser popup window. It can be a number denoting a value in
525  * pixels or a percent string.
526  * @name CKEDITOR.config.filebrowserWindowHeight
527  * @type Number|String
528  * @default <code>'70%'</code>
529  * @example
530  * config.filebrowserWindowHeight = 580;
531  * @example
532  * config.filebrowserWindowHeight = '50%';
533  */
534 