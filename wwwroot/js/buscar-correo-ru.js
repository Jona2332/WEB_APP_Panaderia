function BuscarCorreo() {
    let Correo = $("#Usuario.correo").val();
    $("#btnRegistrarUsuario").prop("disabled", true);

    if (Correo.trim() != "") {
        $.ajax({
            url: '/Usuarios/BuscarExisteCorreo',
            data: {
                "Correo": Correo
            },
            type: 'GET',
            dataType: 'text',
            success: function (res) {

                if (res == '2') {
                    $("#alertaCorreo").html('<p class="text-danger"> Este correo electrónico ya está registrado en el sistema</p>');
                    $("#btnRegistrarUsuario").prop("disabled", true);
                }

                else if (res == '1') {
                    $("#alertaCorreo").html('<p class="text-danger"> El usuario existe en el sistema, pero la cuenta está inactiva. Edite el estado del usuario a activo </p>');
                    $("#btnRegistrarUsuario").prop("disabled", true);
                }

                else {
                    /* alert("El usuario no existe" + res)*/
                    $("#alertaCorreo").html('');
                    $("#btnRegistrarUsuario").prop("disabled", false);

                }
            }
        });
    }
}