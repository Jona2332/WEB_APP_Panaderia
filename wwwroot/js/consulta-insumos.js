$(document).ready(function () {
    $.ajax({
        url: '/Insumos/ConsultaTipoInsumo',
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $('.tipo-insumo').append('<option value="">Seleccione Tipo de Insumo</option>');
            $.each(res, function (key, value) {

                $('.tipo-insumo').append('<option value="' + value.idTipoInsumo + '">' + value.descripcion + '</option>');
            });
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: '/Insumos/ConsultaMarca',
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $('.marca').append('<option value="">Seleccione Marca</option>');
            $.each(res, function (key, value) {

                $('.marca').append('<option value="' + value.idMarca + '">' + value.descripcion + '</option>');
            });
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: '/Insumos/ConsultaPresentacion',
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $('.presentacion').append('<option value="">Seleccione Presentación</option>');
            $.each(res, function (key, value) {

                $('.presentacion').append('<option value="' + value.idPresentacion + '">' + value.descripcion + '</option>');
            });
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: '/Insumos/ConsultaCategoriaInsumo',
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $('.categoria-insumo').append('<option value="">Seleccione Categoría</option>');
            $.each(res, function (key, value) {

                $('.categoria-insumo').append('<option value="' + value.idCategoriaInsumos + '">' + value.descripcion + '</option>');
            });
        }
    });
});

function cargarDatosReporte(idInsumo) {

    // Limpiamos las opciones actuales
    $('.tipo-insumo-editar').empty();
    $('.marca-editar').empty();
    $('.presentacion-editar').empty();
    $('.categoria-insumo-editar').empty();


    $(document).ready(function () {
        $.ajax({
            url: '/Insumos/ConsultaTipoInsumo',
            type: 'GET',
            dataType: 'json',
            success: function (res) {

                $.each(res, function (key, value) {

                    $('.tipo-insumo-editar').append('<option value="' + value.idTipoInsumo + '">' + value.descripcion + '</option>');
                });
            }
        });
    });

    $(document).ready(function () {
        $.ajax({
            url: '/Insumos/ConsultaMarca',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                $.each(res, function (key, value) {

                    $('.marca-editar').append('<option value="' + value.idMarca + '">' + value.descripcion + '</option>');
                });
            }
        });
    });

    $(document).ready(function () {
        $.ajax({
            url: '/Insumos/ConsultaCategoriaInsumo',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                $.each(res, function (key, value) {

                    $('.categoria-insumo-editar').append('<option value="' + value.idCategoriaInsumos + '">' + value.descripcion + '</option>');
                });
            }
        });
    });

    $(document).ready(function () {
        $.ajax({
            url: '/Insumos/ConsultaPresentacion',
            type: 'GET',
            dataType: 'json',
            success: function (res) {

                $.each(res, function (key, value) {

                    $('.presentacion-editar').append('<option value="' + value.idPresentacion + '">' + value.descripcion + '</option>');
                });
            }
        });
    });


    $.ajax({
        url: '/Insumos/ConsultaUnRegistroInsumo/',
        type: 'GET',
        data: { idInsumo: idInsumo },
        success: function (res) {
            console.log(res)
            // Añadimos la opción correspondiente al tratamiento residuo y lo seleccionamos
            //$('.tipo-insumo-editar').append(res.nombre);
            $('.nombre-insumo-editar').val(res.nombre);

            // Añadimos la opción correspondiente al tratamiento residuo y lo seleccionamos
            $('.tipo-insumo-editar').append('<option value="' + res.idTipoInsumo + '">' + res.tipo + '</option>');
            $('.tipo-insumo-editar').val(res.idTipoInsumo);

            // Añadimos la opción correspondiente a la disposición final y lo seleccionamos
            $('.marca-editar').append('<option value="' + res.idMarca + '">' + res.marca + '</option>');
            $('.marca-editar').val(res.idMarca);

            // Añadimos la opción correspondiente al transporte y lo seleccionamos
            $('.categoria-insumo-editar').append('<option value="' + res.idCategoriaInsumos + '">' + res.categoria + '</option>');
            $('.categoria-insumo-editar').val(res.idCategoriaInsumos);

            // Añadimos la opción correspondiente a la categoría desecho y lo seleccionamos
            $('.presentacion-editar').append('<option value="' + res.idPresentacion + '">' + res.presentacion + '</option>');
            $('.presentacion-editar').val(res.idPresentacion);

            // Establecemos el valor del input oculto para el idEvento
            $('#idInsumoEditar').val(res.idInsumo);

            // Establecemos el valor de la fecha en el input datetimepicker
            // Establece la fecha correctamente
            //$('#fechaRevisionEditar').val(res.fechaRevision.split('T')[0]);

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

function confirmarEliminacion(idInsumo) {
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
    }).then((result) => {
        if (result.isConfirmed) {
            eliminarReporte(idInsumo);
        }
    });
}

function eliminarReporte(idInsumo) {
    $.ajax({
        url: '/Insumos/BorrarInsumos',
        type: 'POST',
        data: { idInsumo: idInsumo },
        success: function (data) {
            if (data.success) {
                Swal.fire({
                    icon: "success",
                    title: "\u00A1Eliminado!",
                    text: "Tu archivo ha sido eliminado.",
                    confirmButtonClass: "btn btn-success",
                }).then(function () {
                    location.reload();
                });
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
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Ocurrió un error al intentar eliminar el registro.",
                confirmButtonClass: "btn btn-danger",
            });
        }
    });
}