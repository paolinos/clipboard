(function(){

    var app = new Vue({
        el: '#app',
        data: {
            title: 'Clipbaord Name:',
            clipboard_txt:""
        }
    });


    //--------------------------------------------------------------------------------
    /*          Paste text from clipboard           */
    /*
            Paste logic.
            events and method to copy text memory
            //  TODO: Move to class, clean code.
    */
    //-------------------------------------
    //window.clipboardData.getData('Text')

    var clipbaordArea = document.getElementById("clipbaordArea");

    var isControl = true;
    document.body.addEventListener("keydown", function(e){
        console.log("keydown", e);
        if(e.keyCode === 17){
            isControl = true;
        }else if(e.keyCode === 86){
            //console.log("ctrl+v", window.clipboardData.getData('Text'));
            //console.log("ctrl+v", window.clipboardData);

            clipbaordArea.focus();
            document.execCommand("paste");

        }
    });
    
    document.body.addEventListener("keyup", function(e){
        if(e.keyCode === 17){
            isControl = false;
        }
    });







    //--------------------------------------------------------------------------------
    /*          Drag & Drop           */
    /*
            Drag and drop code.
            events and method to upload the files
            //  TODO: Move to class, clean code.
    */
    //-------------------------------------



    var uploadFiles = function(listFiles)
    {
        var formData = new FormData();
        //var imagefile = document.querySelector('#file');
        formData.append("id", "aa");

        for (let index = 0; index < listFiles.length; index++) {
            const item = listFiles[index];
            formData.append("files", item.getAsFile());
            console.log("index",index, item);
        }
        
        axios.post('/home/add', formData, {
            headers: {
            'Content-Type': 'multipart/form-data'
            }
        }).then(function (response) {
            // handle success
            console.log(response);
          });
    }

    window.addEventListener("dragover",function(e){
        e = e || event;
        e.preventDefault();
    },false);
        window.addEventListener("drop",function(e){
        e = e || event;
        e.preventDefault();
    },false);

    //
    // Drag and Drop files - Html5 
    //      more info:
    //          https://www.html5rocks.com/en/tutorials/dnd/basics/
    //
    var dragFile = document.getElementById("dragFile");
    function handleDragStart(e) {
        console.log("handleDragStart");
        //this.style.opacity = '0.4';  // this / e.target is the source node.
    }  
    function handleDragOver(e) {
        console.log("handleDragOver", e);
        if (e.preventDefault) {
            e.preventDefault(); // Necessary. Allows us to drop.
        }    
        e.dataTransfer.dropEffect = 'move';  // See the section on the DataTransfer object.

        dragFile.removeEventListener('dragover', handleDragOver, false);
        /*
        for (let i = 0; i < e.dataTransfer.items.length; i++) {
            const item = e.dataTransfer.items[i];
            console.log("item", i, item);
        }
        uploadFiles(e.dataTransfer.items);
        */

        return false;
    }
    function handleDragEnter(e) {
        console.log("handleDragEnter", e);
        // this / e.target is the current hover target.
        //this.classList.add('over');
        dragFile.addEventListener('dragover', handleDragOver, false);
    }
    function handleDragLeave(e) {
        console.log("handleDragLeave", e);
        //this.classList.remove('over');  // this / e.target is previous target element.
    }



    var handleDragExit = function(e){
        console.log("handleDragExit", e);
    }
    var handleDragEnd = function(e){
        console.log("handleDragEnd", e);
    }
    var handleDrop = function(e){
        console.log("handleDrop", e.dataTransfer);

        //uploadFiles(e.dataTransfer.files);
        uploadFiles(e.dataTransfer.items);
    }

    dragFile.addEventListener('dragstart', handleDragStart, false);
    dragFile.addEventListener('dragenter', handleDragEnter, false);
    //dragFile.addEventListener('dragover', handleDragOver, false);
    dragFile.addEventListener('dragleave', handleDragLeave, false);
    //dragFile.addEventListener('dragexit', handleDragExit, false);
    //dragFile.addEventListener('dragend', handleDragEnd, false);
    dragFile.addEventListener('drop', handleDrop, false);



})();