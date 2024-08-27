$(document).ready(function () {
    $.ajax({
        url: '/UsuariosRoles/ConsultarUsuariosRoles',
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            function capitalizeFirstLetter(string) {
                return string.charAt(0).toUpperCase() + string.slice(1);
            }
            $('.roles').append('<option value="">Roles</option>');
            $.each(res, function (key, value) {
                var descripcionFormateada = capitalizeFirstLetter(value.descripcion);
                $('.roles').append('<option value="' + value.idRol + '">' + descripcionFormateada + '</option>');
            });
        }
    });
});
