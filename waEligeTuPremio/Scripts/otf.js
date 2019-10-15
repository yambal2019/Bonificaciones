

$(function () {



    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
            
        };

        $.ajax(options).done(function (data) {

            llamarOpen();
            var $target = $($form.attr("data-otf-target"));
            var $newHtml = $(data);
         
            
            $target.replaceWith($newHtml);
            
        });


        return false;
    };


    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);

    


});