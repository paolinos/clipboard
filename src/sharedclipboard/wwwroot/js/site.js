(function(){

    /**
     * Tasks to do:
     * Create new board
     * Load all boards viewed and open it in other tab
     * 
     */
    var app = new Vue({
      el: '#main-content',
      data: {
        title: 'Welcome to Clipbaord',
        isNewBoard: false,
        url_text:"",
        url_name:""
      },
      methods: {
        newBoard: function (event) {
            var _this = this;
            axios.post('home/create').then(function (response) {
                // handle success
                //console.log("create", response);
                _this.isNewBoard = false;

                if(response.status === 200){
                    _this.url_text = response.data;
                    _this.url_name = response.data;
                    _this.isNewBoard = true;
                }
            });
        }
      }
    });

    


    
    
})();