CKEDITOR.editorConfig = function (config) {
    config.language = 'en';
    //config.uiColor = '#F7B42C';
    config.height = 300;
    config.toolbarCanCollapse = true;
    config.extraPlugins = 'uploadimage';
    config.uploadUrl = '/UploadFiles';
    config.filebrowserUploadUrl = '/UploadFiles';
};