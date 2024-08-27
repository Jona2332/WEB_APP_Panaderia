$(document).ready(function () {
    $.ajax({
        url: '/RegistroDesechos/ConsultarCategoriaDesecho',
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $('.categoria-desecho').append('<option value="">Categoria Desecho</option>');
            $.each(res, function (key, value) {

                $('.categoria-desecho').append('<option value="' + value.idCategoria + '">' + value.descripcion + '</option>');
            });
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: '/RegistroDesechos/ConsultarCategoriaDesechoTratamiento',
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $('.tratamiento-residuo').append('<option value="">Cat. Desecho Tratamiento</option>');
            $.each(res, function (key, value) {

                $('.tratamiento-residuo').append('<option value="' + value.idCategoriaTratamientoResiduo + '">' + value.descripcion + '</option>');
            });
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: '/RegistroDesechos/ConsultarDesechoDisposicionFinal',
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $('.disposicion-final').append('<option value="">Disposicion Final</option>');
            $.each(res, function (key, value) {

                $('.disposicion-final').append('<option value="' + value.idDisposicionFinal + '">' + value.descripcion + '</option>');
            });
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: '/RegistroDesechos/ConsultarDesechoTransporte',
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $('.transporte').append('<option value="">Transporte</option>');
            $.each(res, function (key, value) {

                $('.transporte').append('<option value="' + value.idDesechoTransporte + '">' + value.descripcion + '</option>');
            });
        }
    });
});

function cargarDatosReporteDesecho(id) {

    // Limpiamos las opciones actuales
    $('.tratamiento-residuo-editar').empty();
    $('.disposicion-final-editar').empty();
    $('.transporte-editar').empty();
    $('.categoria-desecho-editar').empty();

    $(document).ready(function () {
        $.ajax({
            url: '/RegistroDesechos/ConsultarCategoriaDesecho',
            type: 'GET',
            dataType: 'json',
            success: function (res) {

                $.each(res, function (key, value) {

                    $('.categoria-desecho-editar').append('<option value="' + value.idCategoria + '">' + value.descripcion + '</option>');
                });
            }
        });
    });

    $(document).ready(function () {
        $.ajax({
            url: '/RegistroDesechos/ConsultarCategoriaDesechoTratamiento',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                $.each(res, function (key, value) {

                    $('.tratamiento-residuo-editar').append('<option value="' + value.idCategoriaTratamientoResiduo + '">' + value.descripcion + '</option>');
                });
            }
        });
    });

    $(document).ready(function () {
        $.ajax({
            url: '/RegistroDesechos/ConsultarDesechoDisposicionFinal',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                $.each(res, function (key, value) {

                    $('.disposicion-final-editar').append('<option value="' + value.idDisposicionFinal + '">' + value.descripcion + '</option>');
                });
            }
        });
    });

    $(document).ready(function () {
        $.ajax({
            url: '/RegistroDesechos/ConsultarDesechoTransporte',
            type: 'GET',
            dataType: 'json',
            success: function (res) {

                $.each(res, function (key, value) {

                    $('.transporte-editar').append('<option value="' + value.idDesechoTransporte + '">' + value.descripcion + '</option>');
                });
            }
        });
    });


    $.ajax({
        url: '/RegistroDesechos/ConsultarUnRegistroDesecho/',
        type: 'GET',
        data: { id: id },
        success: function (res) {

            // Añadimos la opción correspondiente al tratamiento residuo y lo seleccionamos
            $('.tratamiento-residuo-editar').append('<option value="' + res.idCategoriaTratamientoResiduo + '">' + res.tratamientoResiduo + '</option>');
            $('.tratamiento-residuo-editar').val(res.idCategoriaTratamientoResiduo);

            // Añadimos la opción correspondiente a la disposición final y lo seleccionamos
            $('.disposicion-final-editar').append('<option value="' + res.idDisposicionFinal + '">' + res.disposicionFinal + '</option>');
            $('.disposicion-final-editar').val(res.idDisposicionFinal);

            // Añadimos la opción correspondiente al transporte y lo seleccionamos
            $('.transporte-editar').append('<option value="' + res.idDesechoTransporte + '">' + res.transporte + '</option>');
            $('.transporte-editar').val(res.idDesechoTransporte);

            // Añadimos la opción correspondiente a la categoría desecho y lo seleccionamos
            $('.categoria-desecho-editar').append('<option value="' + res.idCategoria + '">' + res.categoria + '</option>');
            $('.categoria-desecho-editar').val(res.idCategoria);

            // Establecemos el valor del input oculto para el idEvento
            $('#idEventoEditar').val(res.idEvento);

            // Establecemos el valor de la fecha en el input datetimepicker
            // Establece la fecha correctamente
            $('#fechaRevisionEditar').val(res.fechaRevision.split('T')[0]);

            $('#editar-reporte').modal('show'); // Esto abrirá el modal después de cargar los datos
        },
        error: function (error) {
            console.log('Error al cargar los datos del proveedor:', error);
        }
    });
}
function formatoFecha() {
    $('#fechaRevisionEditar').datetimepicker({
        dateFormat: 'yy-mm-dd' // Formato deseado
    });
}

$(document).ready(function () {
    $(".confirm-text-reporte").on("click", function () {
        var id = $(this).data('id-reporte');
        console.log("ID a eliminar:", id);

        Swal.fire({
            title: "¿Está seguro de eliminar el registro?",
            text: "¡No podrás revertir el cambio!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "¡Sí, eliminar!",
            cancelButtonText: "Cancelar",
            confirmButtonClass: "btn btn-primary",
            cancelButtonClass: "btn btn-danger ml-1",
            buttonsStyling: false,
        }).then(function (result) {
            if (result.isConfirmed) {
                console.log("Confirmación recibida para eliminar el ID:", id);
                eliminarRepDesecho(id);
            }
        });
    });
});

function eliminarRepDesecho(id) {
    console.log("Eliminando reporte con ID:", id);
    $.ajax({
        url: '/RegistroDesechos/EliminarRegistroDesechos',
        type: 'POST',
        data: { idEvento: id },
        success: function (data) {
            console.log("Respuesta recibida:", data);
            if (data.success) {
                Swal.fire({
                    icon: "success",
                    title: "¡Eliminado!",
                    text: "Tu archivo ha sido eliminado.",
                    confirmButtonClass: "btn btn-success",
                }).then(function () {
                    location.reload();
                });;
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: data.message,
                    confirmButtonClass: "btn btn-danger",
                });
            }
        },
        error: function (error) {
            console.log("Error en la llamada AJAX:", error);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Ocurrió un error al intentar eliminar el registro.",
                confirmButtonClass: "btn btn-danger",
            });
        }
    });
}
