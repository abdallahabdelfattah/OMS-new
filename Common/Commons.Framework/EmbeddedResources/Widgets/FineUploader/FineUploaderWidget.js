//class constructor
FineUploaderWidget = function (settings) {

    //private variables setters
    //initialize class logic here
    this.settings = settings;
   
    //init elemets
    this.eleContainer = $('#' + this.settings.containerId);
    this.eleUploader = $('#' + this.settings.containerId + ' .fine-uploader-input');

    this.load();

};

//class members
FineUploaderWidget.prototype =
{

    //widget load entry point
    load: function () {

        var self = this;

        if (!self.eleUploader) return;

        var restrictedUploader = new qq.FineUploader({
            element: this.eleUploader[0],
            template: 'qq-template-validation',
            request: {
                endpoint: '/Files/Upload/'
            },
            thumbnails: {
                placeholders: {
                    waitingPath: '/EmbeddedResources/v1.1/Widgets/FineUploader/css/placeholders/waiting-generic.png',
                    notAvailablePath:
                        '/EmbeddedResources/v1.1/Widgets/FineUploader/css/placeholders/not_available-generic.png'
                }
            },
            // read from our validation attributes
            validation: {
                //autoUpload: true,
                //multiple: true,
                allowedExtensions: ['jpeg', 'jpg', 'txt'],
                itemLimit: 4,
                sizeLimit: 1024 * 1024 * 1024 * 10 // 10MB
            },

            //messages: {
            //    tooManyItemsError: 'You can only add 3 images'
            //},
            debug: true,
            deleteFile: {
                enabled: true,
                method: 'post',
                endpoint: "/Files/Delete/"
            },
            //session: {
            //    refreshOnRequest: true
            //},
            //retry: {
            //    enableAuto: false
            //},
            //scaling: {
            //    sendOriginal: true,
            //    hideScaled: true,
            //    sizes: [
            //        { name: "THUMB_XX", maxSize: 113 },
            //        { name: "FULLIMAGE", maxSize: 450 }
            //    ]
            //},
            callbacks: {
                onError: function (id, name, errorReason, xhrOrXdr) {
                    //alert(qq.format("Error on file number {} - {}.  Reason: {}", id, name, errorReason));
                },
                onDelete: function (id) {
                    this.setDeleteFileParams({ filename: this.getName(id) }, id);
                },
                onDeleteComplete: function (id, xhr, isError) {
                    alert('ON DELETE COMPLETE ' + id);
                },
                onComplete: function (id, xhr, isError) {
                    alert('ON COMPLETE ' + id);
                },
                onAllComplete: function (id, xhr, isError) {
                    alert('ON ALL COMPLETE ' + id);
                }
            }
        });
        
    } //load End.

}; //class end