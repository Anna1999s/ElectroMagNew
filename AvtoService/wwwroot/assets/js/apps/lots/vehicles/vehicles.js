$(document).ready(function () {
    $('#type').change(function () {
        $('#mark option').remove();
        $.getJSON('/Admin/Lot/GetVehicleMarks', { id: $('#type').val() }, function (marks) {
            $.each(marks, function () {
                $('#mark').append('<option value=' +
                    this.id + '>' + this.name + '</option>');
            });
        });
    });

    $('#mark').change(function () {
        $('#model option').remove();
        $.getJSON('/Admin/Lot/GetVehicleModels', { id: $('#mark').val() }, function (models) {
            $.each(models, function () {
                $('#model').append('<option value=' +
                    this.id + '>' + this.name + '</option>');
            });
        });
    });

    $('#type').change(function () {
        $('#drive-type option').remove();
        $.getJSON('/Admin/Lot/GetDriveTypes', { id: $('#type').val() }, function (driveTypes) {
            $.each(driveTypes, function () {
                $('#drive-type').append('<option value=' +
                    this.id + '>' + this.name + '</option>');
            });
        });
    });

    $('#type').change(function () {
        $('#options option').remove();
        $.getJSON('/Admin/Lot/GetVehicleOptions', { id: $('#type').val() }, function (options) {
            $.each(options, function () {
                $('#options').append('<option value=' +
                    this.id + '>' + this.name + '</option>');
            });
        });
    });

    $('#type').change(function () {
        $('#body-type option').remove();
        $.getJSON('/Admin/Lot/GetBodyTypes', { id: $('#type').val() }, function (bodyTypes) {
            $.each(bodyTypes, function () {
                $('#body-type').append('<option value=' +
                    this.id + '>' + this.name + '</option>');
            });
        });
    });

    $('#type').change(function () {
        $('#transmission-type option').remove();
        $.getJSON('/Admin/Lot/GetTransmissionTypes', { id: $('#type').val() }, function (transmissionTypes) {
            $.each(transmissionTypes, function () {
                $('#transmission-type').append('<option value=' +
                    this.id + '>' + this.name + '</option>');
            });
        });
    });

    $(function () {
        $('input[id="sum-buy-now"]').hide();

        //show it when the checkbox is clicked
        $('input[id="check-buy-now"]').on('click', function () {
            $('input[id="sum-buy-now"]').val(null);
            if ($(this).prop('checked')) {
                $('input[id="sum-buy-now"]').fadeIn();

            } else {
                $('input[id="sum-buy-now"]').hide();
            }
        });
    });
});