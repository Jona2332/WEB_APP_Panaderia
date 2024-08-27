function consultarDetallesOrden(idOrden) {
    $.ajax({
        url: '/OrdenPDV/ConsultarDetalleOrdenPDV?idOrden=' + idOrden,
        type: 'GET',
        data: { id: idOrden },
        success: function (response) {
            console.log(response)
            // Procesar la respuesta y actualizar el modal
            actualizarModal(response);
            // Mostrar el modal
            $('#detalle-orden').modal('show');
        },
        error: function (xhr, status, error) {
            console.error('Error al consultar detalles de la orden:', error);
            console.log(xhr.responseText);
        }
    });
}

function actualizarModal(data) {
    $(document).ready(function () {
        $.ajax({
            url: '/OrdenPDV/ConsultarEstadosOrden',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                var select = $('#id_estado');
                select.empty();
                $.each(res, function (key, value) {
                    select.append('<option value="' + value.idEstadoOrden + '">' + value.estado + '</option>');
                });

                var orden = data[0]; // Acceder al primer elemento del array
                // Actualizar el estado de la orden
                var estadoClass = orden.estado === "Pendiente" ? "text-bg-secondary" : orden.estado === "En Progreso" ? "text-bg-warning" : "text-bg-success";
                var estadoSpan = $('.estado-span');
                estadoSpan.text(orden.estado).attr('class', 'badges ' + estadoClass);

                // Seleccionar el estado actual en el select
                select.val(orden.estado);

                // Mostrar el select cuando se hace clic en el span
                estadoSpan.on('click', function () {
                    estadoSpan.hide();
                    select.show().focus();
                });

                // Cambiar el estado del span cuando se cambia el select
                select.on('change', function () {
                    var selectedText = $(this).find('option:selected').text();
                    var selectedClass = selectedText === "Pendiente" ? "text-bg-secondary" : selectedText === "En Progreso" ? "text-bg-warning" : "text-bg-success";
                    estadoSpan.text(selectedText).attr('class', 'badges ' + selectedClass).show();
                    select.hide();
                });
            }
        });
    });

    if (data.length > 0) {
        var orden = data[0]; // Acceder al primer elemento del array

        // Actualizar los campos del cliente
        $('#detalle-orden input[placeholder="Cliente"]').val(orden.nombreCliente || "");
        $('#detalle-orden textarea[placeholder="Dirección"]').val(orden.direccion || "");
        $('#detalle-orden input[placeholder="Teléfono"]').val(orden.telefono || "");

        // Actualizar el tipo de orden (Express/Cliente-Salón)
        $('#detalle-orden .badge.bg-warning').text(orden.express ? "Express" : "Cliente-Salón");

        // Actualizar el número de la orden
        $('#detalle-orden .badge.bg-info').text('#' + (orden.idOrden || ""));

        // Limpiar la tabla antes de agregar los nuevos datos
        $('#detalle-orden .table tbody').empty();

        // Renderizar los detalles del producto
        var detalleHtml = '';
        data.forEach(function (detalle) {
            detalleHtml += `
                <tr>
                    <td>${detalle.nombreProducto || ""}</td>
                    <td>${detalle.cantidad || ""}</td>
                    <td>${detalle.tipo || ""}</td>
                    <td>${detalle.sabores || ""}</td>
                    <td>${detalle.nota || ""}</td>
                </tr>
            `;
        });
        $('#detalle-orden .table tbody').append(detalleHtml);
    } else {
        console.error('La respuesta no contiene datos.');
    }
}

// Manejar el clic en el botón de Actualizar Orden
$('#actualizar-orden-btn').on('click', function () {
    var idOrden = $('#detalle-orden .badge.bg-info').text().replace('#', '').trim();
    var idEstadoOrden = $('.estado-orden').val();

    $.ajax({
        url: '/OrdenPDV/ActualizarEstadoOrden',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            idOrden: idOrden,
            idEstadoOrden: idEstadoOrden
        }),
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: '¡Éxito!',
                    text: 'El estado de la orden se ha actualizado correctamente.',
                }).then((result) => {
                    if (result.isConfirmed) {
                        location.reload();
                    }
                });
                // Cerrar el modal o realizar otras acciones necesarias
                $('#detalle-orden').modal('hide');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Hubo un problema al actualizar el estado de la orden.',
                }).then((result) => {
                    if (result.isConfirmed) {
                        location.reload();
                    }
                });
            }
        },
        error: function (xhr, status, error) {
            console.error('Error al actualizar el estado de la orden:', error);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Hubo un problema al actualizar el estado de la orden: ' + xhr.responseText,
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload();
                }
            });
        }
    });
});

