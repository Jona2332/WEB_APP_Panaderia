function validarCampoCorreo() {
    var correo = $("#Usuario.correo").val();
    $("#btnRegistrarUsuario").prop("disabled", true);

    if (correo == null || correo == "") {
        $("#alertaCorreo2").html('<p class="text-danger">Ingrese un correo electronico</p>');
    }
    else if (!(validarFormatoCorreo(correo))) {
        $("#alertaCorreo2").html('<p class="text-danger">Ingrese un correo electronico válido</p>');
    }

    else {
        $("#alertaCorreo2").html('');
        $("#btnRegistrarUsuario").prop("disabled", false);
    }

}

function validarFormatoCorreo(correo) {

    var x = correo;
    var atposition = x.indexOf("@");
    var dotposition = x.lastIndexOf(".");
    if (atposition < 1 || dotposition < atposition + 2 || dotposition + 2 >= x.length) {
        return false;
    } else { return true; }
}
