(function ($) {
    "use strict";
    
    /*File Pond*/
    if( $('.file-pond').length ) {
        FilePond.registerPlugin(FilePondPluginImageExifOrientation, FilePondPluginImagePreview);
        const inputElement = document.querySelector('.file-pond');
        const pond = FilePond.create( inputElement, {
            maxFiles: 1,
            acceptedFileTypes: ['image/*'],
            imagePreviewHeight: 140,
            onprocessfiles: (error, file) => {
                if (!error) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        const img = document.getElementById('preview');
                        img.src = e.target.result;
                        document.getElementById('imagePreview').style.display = 'block';
                    };
                    reader.readAsDataURL(file.file);
                } else {
                    console.error('Upload error:', error);
                }
            }
        });
    }
    
})(jQuery);