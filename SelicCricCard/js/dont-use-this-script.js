var langDOM = document.getElementById("langChange"),
	language = 'en',
	TRANSLATE = {};
	swap = function (x){return x};
load_lang(language);
function load_lang(l) {
	var src = 'js/'+l+'.json.js';
  var head = document.getElementsByTagName('head')[0];

  //use class, as we can't reference by id
  var element = head.getElementsByClassName("json")[0];

  try {
    element.parentNode.removeChild(element);
  } catch (e) {
    //
  }

  var script = document.createElement('script');
  script.type = 'text/javascript';
  script.src = src;
  script.className = "json";
  script.async = false;
  head.appendChild(script);

  //call the postload function after a slight delay to allow the json to load
  window.setTimeout(applyLang, 100);
}

function translate(elm, translation){
	switch(elm.tagName.toLowerCase()){
		case 'input': 
		case 'textarea':
			if(elm.placeholder)
				elm.placeholder = translation;
			else if(elm.value)
				elm.value = translation;
			break;
		default: 
			elm.textContent = translation;
	}
}
function applyLang(){
	var dataLang = document.querySelectorAll('[data-lang]');
	for(var i=0; i<dataLang.length; i++){
		var key = dataLang[i].dataset.lang;
		translate(dataLang[i], TRANSLATE[key])
	}
}

function changeLang(lang, elm){
	language = lang;
	load_lang(lang);
	var langNodes = langDOM.children;
	for(var i =0; i<langNodes.length; i++){
		removeClass('active', langNodes[i]);
	}
	addClass('active', elm);
}

function goto(page){
	document.location.href = page;
}

function loadMore(){
	var showScore = document.getElementById("hideScore");
	removeClass('hide', showScore);
}

function toggleExc(strClassName, item1, item2){
	if ( hasClass(item1) )
		item2 = swap(item1, item1=item2);
	removeClass(strClassName, item2);
	addClass(strClassName, item1);
}
function hasClass(strClassName, item){
	var regx = new RegExp('(?:^|\\s)'+strClassName+'(?!\\S)','g');
	return item.className.match(regx);
}
function removeClass(strClassName, item){
	var regx = new RegExp('(?:^|\\s)'+strClassName+'(?!\\S)','g');
	item.className = item.className.replace( regx, '' );
}
function addClass(strClassName, item) {
	item.className += " " + strClassName;
}

