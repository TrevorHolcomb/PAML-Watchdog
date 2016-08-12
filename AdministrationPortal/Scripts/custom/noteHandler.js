// encapsulates methods used in Views/Rules/Create and Views/Rules/Edit

var noteHandler = {

    //Toggle last visible button for added notes
    makeLastVisibleButtonGreenNewNotes: function() {
        var $lastEnabled = $('.addedNotes').find('.parameter-enabled').filter("[value='True']").last();
        var $lastButton = $lastEnabled.next('.input-group').find('.btn');
        $lastButton.removeClass('btn-danger btn-remove-new');
        $lastButton.addClass('btn-success btn-add-new').html('+');
    },

    //Toggle last visible button for selected notes
    makeLastVisibleButtonGreenSelectedNotes: function() {
        var $lastEnabled = $('.selectedNotes').find('.parameter-enabled').filter("[value='True']").last();
        var $lastButton = $lastEnabled.next('.input-group').find('.btn');
        $lastButton.removeClass('btn-danger btn-remove-selected');
        $lastButton.addClass('btn-success btn-add-selected').html('+');
    },

    //add selected note group
    addSelectedFormGroup: function(event) {
        event.preventDefault();

        var $multipleFormGroupSelected = $(this).closest('.multiple-form-group-selected');

        //Toggle this button state
        $(this).toggleClass('btn-success btn-add-selected btn-danger btn-remove-selected').html('–');

        //Enable the next group
        var $nextGroup = $multipleFormGroupSelected.next('.multiple-form-group-selected');
        $nextGroup.find('.parameter-enabled').attr('value', 'True');
        $nextGroup.find('.btn').toggleClass('btn-success btn-add-selected btn-danger btn-remove-selected').html('+');
        $nextGroup.fadeIn(300);

        //Always disable the last button
        var $lastFormGroup = $multipleFormGroupSelected.siblings('.multiple-form-group-selected:last');
        $lastFormGroup.find('.btn').attr('disabled', true);
        makeLastVisibleButtonGreenSelectedNotes();
    },

    //remove selected note group
    removeSelectedFormGroup: function(event) {
        event.preventDefault();

        var $multipleFormGroupSelected = $(this).closest('.multiple-form-group-selected');

        //Always reenable the last button before replacing it
        var $lastFormGroup = $multipleFormGroupSelected.siblings('.multiple-form-group-selected:last');
        $lastFormGroup.find('.btn').attr('disabled', false);

        //Disable this group and move it to the bottom
        $multipleFormGroupSelected.fadeOut(300,
            function() {
                $multipleFormGroupSelected.find('.text-box').val('');
                $multipleFormGroupSelected.find('.parameter-enabled').attr('value', 'False');
                $multipleFormGroupSelected.insertAfter($lastFormGroup);
            });
        makeLastVisibleButtonGreenSelectedNotes();
    },

    //add new note group
    addNewFormGroup: function(event) {
        event.preventDefault();

        var $multipleFormGroupNew = $(this).closest('.multiple-form-group-new');

        //Toggle this button state
        $(this).toggleClass('btn-success btn-add-new btn-danger btn-remove-new').html('–');

        //Enable the next group
        var $nextGroup = $multipleFormGroupNew.next('.multiple-form-group-new');
        $nextGroup.find('.parameter-enabled').attr('value', 'True');
        $nextGroup.find('.btn').toggleClass('btn-success btn-add-new btn-danger btn-remove-new').html('+');
        $nextGroup.fadeIn(300);

        //Always disable the last button
        var $lastFormGroup = $multipleFormGroupNew.siblings('.multiple-form-group-new:last');
        $lastFormGroup.find('.btn').attr('disabled', true);
        makeLastVisibleButtonGreenNewNotes();
    },

    //remove new note group
    removeNewFormGroup: function(event) {
        event.preventDefault();

        var $multipleFormGroupNew = $(this).closest('.multiple-form-group-new');

        //Always reenable the last button before replacing it
        var $lastFormGroup = $multipleFormGroupNew.siblings('.multiple-form-group-new:last');
        $lastFormGroup.find('.btn').attr('disabled', false);

        //Disable this group and move it to the bottom
        $multipleFormGroupNew.fadeOut(300,
            function() {
                $multipleFormGroupNew.find('.text-box').val('');
                $multipleFormGroupNew.find('.parameter-enabled').attr('value', 'False');
                $multipleFormGroupNew.insertAfter($lastFormGroup);
            });
        makeLastVisibleButtonGreenNewNotes();
    }
};