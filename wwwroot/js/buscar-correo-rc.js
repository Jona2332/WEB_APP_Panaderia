function BuscarCorreo() {
    let Correo = $("#correo").val();
    $("#btnRecuperarContrasenna").prop("disabled", true);

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
                    $("#alertaCorreo").html('');
                    $("#btnRecuperarContrasenna").prop("disabled", false);
                }

                else if (res == '1') {
                    $("#alertaCorreo").html('<p class="text-danger"> El usuario existe en el sistema, pero la cuenta está inactiva. Por favor contacte al administrador </p>');
                    $("#btnRecuperarContrasenna").prop("disabled", true);
                }

                else {
                    /* alert("El usuario no existe" + res)*/
                    $("#alertaCorreo").html('<p class="text-danger"> El usuario no existe en el sistema. Por favor contacte al administrador</p>');
                    $("#btnRecuperarContrasenna").prop("disabled", true);

                }
          
                


            }
        });
    }
}