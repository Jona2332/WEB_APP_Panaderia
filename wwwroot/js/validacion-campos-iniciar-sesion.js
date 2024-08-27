function validarCampoCorreo() {
    var correo = $("#correo").val();
    $("#btnIniciarSesion").prop("disabled", true);

    if (correo == null || correo == "") {
        $("#alertaCorreo").html('<p class="text-danger">Ingrese un correo electronico</p>');
    }
    else if (!(validarFormatoCorreo(correo))) {
        $("#alertaCorreo").html('<p class="text-danger">Ingrese un correo electronico válido</p>');
    }

    else {
        $("#alertaCorreo").html('');
        $("#btnIniciarSesion").prop("disabled", false);
    }

}

function validarCampoContrasenna() {
    var contrasenna = $("#contrasenna").val();
    $("#btnIniciarSesion").prop("disabled", true);

    if (contrasenna == null || contrasenna == "") {
        $("#alertaContrasenna").html('<p class="text-danger">Ingrese una contraseña</p>');
    }
    else { $("#alertaContrasenna").html(''); $("#btnIniciarSesion").prop("disabled", false); }
}



function validarFormatoCorreo(correo) {

    var x = correo;
    var atposition = x.indexOf("@");
    var dotposition = x.lastIndexOf(".");
    if (atposition < 1 || dotposition < atposition + 2 || dotposition + 2 >= x.length) {
        return false;
    } else { return true; }
}
