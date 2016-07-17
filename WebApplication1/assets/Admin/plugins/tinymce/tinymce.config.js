tinymce.init({
	selector: "textarea.tinymce-editor",theme: "modern", height: 300,
   entity_encoding : "raw",
   menubar: false,
   statusbar: false,
   image_advtab: true ,

	plugins: [
	"advlist autolink link image lists charmap print preview hr anchor pagebreak",
	"searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking",
	"table contextmenu directionality emoticons paste textcolor responsivefilemanager code"
	],

	toolbar: "table | responsivefilemanager image media | styleselect | bold italic underline fontsizeselect | alignleft aligncenter alignright alignjustify | bullist numlist | forecolor backcolor | link unlink | removeformat | preview code",

   external_filemanager_path: "/quantri/assets/plugins/filemanager/",
   filemanager_title: "Responsive Filemanager" ,
   external_plugins: { "filemanager" : "/quantri/assets/plugins/filemanager/plugin.min.js"},
});