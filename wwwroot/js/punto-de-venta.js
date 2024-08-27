
$(document).ready(function () {
    $("#panaderia").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=1', 
            method: 'GET',
            success: function (response) {
                renderProductosPanaderia(response);
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });


    $("#pizzeria").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=2', 
            method: 'GET',
            success: function (response) {
                renderProductosPizzeria(response, 'pizzeria');
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });

    $("#pastas").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=3',
            method: 'GET',
            success: function (response) {
                renderProductosPastas(response, 'pastas');
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });

    $("#bebidas").click(function () {
        $.ajax({
            url: '/Productos/ConsultarProductosPorCategoria?idCategoria=4',
            method: 'GET',
            success: function (response) {
                renderProductosBebidas(response, 'bebidas');
            },
            error: function (error) {
                console.log("Error al obtener productos", error);
            }
        });
    });


    function renderProductosPanaderia(productos) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card"
             
                    data-producto-id="${producto.idProducto}" 
                    data-producto-nombre="${producto.nombre}" 
                    data-producto-precio="${producto.precioUnitario}" 
                    data-producto-tipo="${producto.tipo}" 
                    data-producto-descripcion="${producto.descripcion}"
                    data-producto-idcategoria="${producto.idCategoria}">
                        <h6 class="cat-name"><a>${producto.tipo}</a></h6>
                        <h6 class="product-name"><a>${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        });
        $(".tab_content[data-tab='panaderia'] .row").html(content);

        // Añadir el evento de clic para los productos
        $(".product-info").click(function () {
            const categoriaId = $(this).data('producto-idcategoria');
            const productoId = $(this).data('producto-id');
            const productoNombre = $(this).data('producto-nombre');
            const productoPrecio = $(this).data('producto-precio');
            const productoTipo = $(this).data('producto-tipo');
            const productoDescripcion = $(this).data('producto-descripcion');
            const sabores = '';
            if (productoTipo.includes("Un tipo") || productoTipo.includes("Uno / dos tipos")) {
                openModal(productoId, productoNombre, productoPrecio, productoDescripcion, categoriaId);
            } else {
                addProductToCart(productoId, productoNombre, productoTipo, productoPrecio, productoDescripcion,sabores,categoriaId);

            }
        });
    }

    function renderProductosPizzeria(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card"
                    data-producto-id="${producto.idProducto}"
                    data-producto-nombre="${producto.nombre}" 
                    data-producto-precio="${producto.precioUnitario}" 
                    data-producto-tipo="${producto.tipo}" 
                    data-producto-idcategoria="${producto.idCategoria}">
                        <h6 class="cat-name"><a>${producto.tipo}</a></h6>
                        <h6 class="product-name"><a>${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <span>${producto.descripcion}</span>
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        })
        $(`.tab_content[data-tab='pizzeria'] .row`).html(content);

        // Añadir el evento de clic para los productos
        $(".product-info").click(function () {
            const categoriaId = $(this).data('producto-idcategoria');
            const productoId = $(this).data('producto-id');
            const productoNombre = $(this).data('producto-nombre');
            const productoPrecio = $(this).data('producto-precio');
            const productoTipo = $(this).data('producto-tipo');
            const productoDescripcion = $(this).data('producto-descripcion');
            const sabores = '';
            if (productoTipo.includes("Un tipo") || productoTipo.includes("Uno / dos tipos")) {
                openModal(productoId, productoNombre, productoTipo, productoPrecio, productoDescripcion, categoriaId);
                      
            } else {
                addProductToCart(productoId, productoNombre, productoTipo, productoPrecio, productoDescripcion, sabores, categoriaId);
            }
        });
    }

    function renderProductosPastas(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card"
                    data-producto-id="${producto.idProducto}"
                    data-producto-nombre="${producto.nombre}"
                    data-producto-precio="${producto.precioUnitario}" 
                    data-producto-tipo="${producto.tipo}"
                    data-producto-idcategoria="${producto.idCategoria}">
                        <h6 class="cat-name"><a>${producto.tipo}</a></h6>
                        <h6 class="product-name"><a>${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <span>${producto.descripcion}</span>
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        })
        $(`.tab_content[data-tab='pastas'] .row`).html(content);

        $(".product-info").click(function () {
            const categoriaId = $(this).data('producto-idcategoria');
            const productoId = $(this).data('producto-id');
            const productoNombre = $(this).data('producto-nombre');
            const productoPrecio = $(this).data('producto-precio');
            const productoTipo = $(this).data('producto-tipo');
            const productoDescripcion = $(this).data('producto-descripcion');
            const sabores = '';
            addProductToCart(productoId, productoNombre, productoTipo, productoPrecio, productoDescripcion, sabores, categoriaId);
                          
        });
    }

    function renderProductosBebidas(productos, categoria) {
        let content = '';
        productos.forEach(function (producto) {
            content += `
                <div class="col-sm-2 col-md-6 col-lg-3 col-xl-3 pe-2">
                    <div class="product-info default-cover card"
                    data-producto-id="${producto.idProducto}" 
                    data-producto-nombre="${producto.nombre}" 
                    data-producto-precio="${producto.precioUnitario}" 
                    data-producto-tipo="${producto.tipo}" 
                    data-producto-descripcion="${producto.descripcion}" 
                    data-producto-idCategoria="${producto.idCategoria}">
                        <h6 class="cat-name"><a>${producto.tipo}</a></h6>
                        <h6 class="product-name"><a>${producto.nombre}</a></h6>
                        <div class="d-flex align-items-center justify-content-between price">
                            <span>${producto.descripcion}</span>
                            <p>₡${producto.precioUnitario}</p>
                        </div>
                    </div>
                </div>
            `;
        })
        $(`.tab_content[data-tab='bebidas'] .row`).html(content);

        $(".product-info").click(function () {
            const categoriaId = $(this).data('producto-idcategoria');
            const productoId = $(this).data('producto-id');
            const productoNombre = $(this).data('producto-nombre');
            const productoPrecio = $(this).data('producto-precio');
            const productoTipo = $(this).data('producto-tipo');
            const productoDescripcion = $(this).data('producto-descripcion');
            const sabores = '';
            addProductToCart(productoId, productoNombre, productoTipo, productoPrecio, productoDescripcion, sabores, categoriaId);                    
            
        });
    }
    // Función para abrir el modal y cargar el contenido dinámico
    function openModal(productoId, productoNombre, productoTipo, productoPrecio, productoDescripcion, categoriaId) {
        $.ajax({
            url: '/SaboresPizza/ConsultarSaboresPizza', 
            method: 'GET',
            success: function (response) {
                renderModalContent(response);
                $('#tipo-pizza').modal('show'); // Mostrar el modal

                // Añadir el evento de clic al botón de aceptar en el modal
                $("#accept-button").off('click').on('click', function () {
                    let selectedSabores = $(".sabor-check:checked").map(function () {
                        return $(this).closest('.row').find('h6').text();
                    }).get();
                    addProductToCart(productoId, productoNombre, productoTipo, productoPrecio, productoDescripcion, selectedSabores.join(', '), categoriaId);
                });

            },
            error: function (error) {
                console.log("Error al obtener sabores de pizza", error);
            }
        });
    }

    

    // Función para renderizar el contenido del modal
    function renderModalContent(sabores) {
        let content = '';
        sabores.forEach(function (sabor) {
            content += `
                 <div class="row align-items-center" data-sabor-id="${sabor.idSaborPizza}">
                    <div class="col-sm-6">
                        <div class="setting-info">
                            <h6>${sabor.nombre}</h6>
                            <p>${sabor.descripcion}</p>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="localization-select d-flex align-items-center">
                            <div class="status-toggle modal-status d-flex justify-content-between align-items-center me-3">
                                <input type="checkbox" id="tipo${sabor.idSaborPizza}" class="check sabor-check" data-sabor-id="${sabor.idSaborPizza}">
                                <label for="tipo${sabor.idSaborPizza}" class="checktoggle"></label>
                            </div>
                        </div>
                    </div>
                </div>

            `;
        });
        $(".dynamic-content").html(content);

        // Añadir el evento de cambio para los checkboxes
        $(".sabor-check").change(function () {
            let checkedCount = $(".sabor-check:checked").length;
            $(".sabor-check:not(:checked)").prop('disabled', checkedCount >= 2);
            $("#accept-button").prop('disabled', checkedCount < 1);
        });


    }

    // Función para agregar productos al carrito
    function addProductToCart(producto_id, nombre, tipo, precio, descripcion, sabores, categoriaId) {
        let descripcionHTML = descripcion ? `<p>Descripcion: ${descripcion}</p>` : '';
        let saboresHTML = sabores ? `<p>Sabores: ${sabores}</p>` : '';
       
            let productHTML = `
                            <div class="product-list d-flex align-items-center justify-content-between"
                            data-productoid="${producto_id}" 
                            data-nombre="${nombre}" 
                            data-tipo="${tipo}"
                            data-sabores="${sabores}"
                            data-precio="${precio}" 
                            data-descripcion="${descripcion}" 
                            data-categoriaid="${categoriaId}">
                                <div class="d-flex align-items-center product-info">
                                    <div class="info">
                                        <h6 class="product-name"><a>${nombre} - ${tipo} </a></h6>
                                        <p hidden class="product-type">${tipo}</p>
                                         ${descripcionHTML}
                                         ${saboresHTML}
                                        <p class="subtotal">₡<span class="subtotal-amount">${precio}</span></p>
                                         <input type="text" class="form-control product-note" placeholder="Nota">
                                    </div>
                                </div>
                                <div class="qty-item text-center">
                                    <input hidden type="text" class="form-control text-center qty-input" name="qty" value="1" readonly>
                                </div>
                                <div class="d-flex align-items-center action">
                                    <a class="btn-icon delete-icon confirm-text" href="javascript:void(0);">
                                        <i data-feather="trash-2" class="feather-14"></i>
                                    </a>
                                </div>
                            </div>
                            `;
            $("#product-list").append(productHTML);
        feather.replace(); // Actualiza los iconos de Feather


        // Añadir manejadores de eventos para los botones de incremento y decremento
        $(".inc").off('click').on('click', function () {
            let qtyInput = $(this).siblings(".qty-input");
            let currentQty = parseInt(qtyInput.val());
            let newQty = currentQty + 1;
            qtyInput.val(newQty);
            updateSubtotal($(this).closest('.product-list'), newQty, precio);
            updateOrderTotal();
        });

        $(".dec").off('click').on('click', function () {
            let qtyInput = $(this).siblings(".qty-input");
            let currentQty = parseInt(qtyInput.val());
            if (currentQty > 1) {
                let newQty = currentQty - 1;
                qtyInput.val(newQty);
                updateSubtotal($(this).closest('.product-list'), newQty, precio);
                updateOrderTotal();
            }
        });



        // Añadir manejador de evento para el botón de eliminar
        $(".delete-icon").off('click').on('click', function () {
            $(this).closest('.product-list').remove();
            updateOrderTotal();
        });

        // Actualizar el total de la orden
        updateOrderTotal();

    }

    // Función para actualizar el subtotal
    function updateSubtotal(productElement, quantity, price) {
        let newSubtotal = quantity * price;
        productElement.find(".subtotal-amount").text(newSubtotal);
    }

    // Función para actualizar el total de la orden
    function updateOrderTotal() {
        let total = 0;
        $(".subtotal-amount").each(function () {
            total += parseFloat($(this).text());
        });
        $(".order-total .text-end").html(`₡${total.toFixed(2)}`);
    }

    // Manejador de clic para remover todos los productos
    $("#remove-all").click(function () {
        $("#product-list").empty();
        updateOrderTotal();
    });

    $(document).ready(function () {

        // Variables globales para almacenar los datos de la orden
        var globalOrderData = {};
        var globalProducts = [];
        let stripe;
        let stripeSessionId;
        $('#payButton').click(function () {
            // Obtener la información de los productos
            var products = [];
            var totalAmount = 0;
            $('.product-list').each(function () {
                var categoriaId = $(this).data('categoriaid');
                var productoId = $(this).data('productoid');
                var productoNombre = $(this).data('nombre');
                var productoPrecio = parseFloat($(this).data('precio'));
                var productoTipo = $(this).data('tipo') || 'N/A';
                var productoSabores = $(this).data('sabores');
                if (productoSabores === 'undefined') {
                    var productoSabores = 'N/A';
                }
                var productoDescripcion = $(this).data('descripcion');
                if (productoDescripcion === 'undefined') {
                    var productoDescripcion = 'N/A';
                }
                var productoCantidad = parseInt($(this).find('.qty-input').val());
                var productoTotal = productoPrecio * productoCantidad;
                totalAmount += productoTotal;
                var productoNota = $(this).find('.product-note').val() || 'N/A';

                products.push({
                    idProducto: productoId,
                    nombre: productoNombre,
                    precio: productoPrecio,
                    tipo: productoTipo,
                    descripcion: productoDescripcion,
                    sabores: productoSabores,
                    cantidad: productoCantidad,
                    total: productoTotal,
                    nota: productoNota
                });
            });

            // Verificar si hay productos
            if (products.length === 0) {
                Swal.fire({
                    icon: 'warning',
                    title: 'No hay productos',
                    text: 'No hay productos por pagar. Por favor, agregue productos al carrito antes de proceder con el pago.',
                    confirmButtonText: 'Aceptar'
                });
                return;
            }

            // Guardar los productos y el total globalmente
            globalProducts = products;
            globalOrderData.totalAmount = totalAmount;

            // Obtener el total
            var total = $('.order-total .text-end').text();

            // Obtener el método de pago seleccionado ---- STRIPE
            var selectedMethod = $('.payment-method .selected-method span').text();

            // Verificar si hay un método de pago seleccionado
            if (!selectedMethod) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Método de pago no seleccionado',
                    text: 'Por favor, seleccione un método de pago antes de proceder con el pago.',
                    confirmButtonText: 'Aceptar'
                });
                return;
            }

            // Verificar si el pedido es express
            var isExpress = $('#express-order-checkbox').is(':checked');

            // Obtener la información del cliente
            var customerNombre = $('#customerNombreHidden').text() || $('#customerNameInput input[name="nombre"]').val() || 'N/A';
            var customerCorreo = $('#customerCorreoHidden').text() || 'N/A';
            var customerTelefono = $('#customerTelefonoHidden').text() || 'N/A';
            var customerDireccion = $('#customerDireccionHidden').text() || 'N/A';

            // Guardar los datos del cliente globalmente
            globalOrderData.customerNombre = customerNombre;
            globalOrderData.customerTelefono = customerTelefono;
            globalOrderData.customerDireccion = customerDireccion;

            // Llenar los datos del cliente en el modal
            $('#ordenCustomerNombre').text(customerNombre);
            $('#ordenCustomerTelefono').text(customerTelefono);
            $('#ordenCustomerDireccion').text(customerDireccion);

            // Llenar la fecha actual en el modal
            var fechaActual = new Date().toLocaleDateString();
            $('#ordenFecha').text(fechaActual);

            // Llenar la tabla de productos en el modal
            var productRows = '';
            products.forEach(function (product, index) {
                var descripcion = (product.descripcion && product.descripcion.trim()) ? product.descripcion : 'N/A';
                var tipo = (product.tipo && product.tipo.trim()) ? product.tipo : 'N/A';
                var sabores = (product.sabores && product.sabores.trim()) ? product.sabores : 'N/A';
                productRows += `
                 <tr>
                    <td>${index + 1}. ${product.nombre}</td>
                    <td>₡${product.precio.toFixed(2)}</td>
                    <td>${product.cantidad}</td>
                    <td>${tipo}</td>
                    <td>${sabores}</td>
                    <td>${descripcion}</td>
                    <td>${product.nota}</td>
                    <td class="text-end">₡${product.total.toFixed(2)}</td>
                </tr>
            `;
            });
            $('#ordenProductList').html(productRows);

            // Llenar el total en el modal
            $('#ordenTotalAmount').text(`₡${totalAmount.toFixed(2)}`);

            // Mostrar el modal
            $('#detalle-orden').modal('show');
        });

        $('#printReceiptButton').click(function () {
            // Llenar los datos del cliente en el modal de factura
            $('#receiptCustomerNombre').text(globalOrderData.customerNombre);
            $('#receiptCustomerTelefono').text(globalOrderData.customerTelefono);
            $('#receiptCustomerDireccion').text(globalOrderData.customerDireccion);

            // Llenar la tabla de productos en el modal de factura
            var receiptProductRows = '';
            globalProducts.forEach(function (product, index) {
                var descripcion = (product.descripcion && product.descripcion.trim()) ? product.descripcion : 'N/A';
                var tipo = (product.tipo && product.tipo.trim()) ? product.tipo : 'N/A';
                receiptProductRows += `
           <tr>
              <td>${index + 1}. ${product.nombre}</td>
              <td>₡${product.precio.toFixed(2)}</td>
              <td>${product.cantidad}</td>
              <td class="text-end">₡${product.total.toFixed(2)}</td>
          </tr>
      `;
            });
            $('#receiptProductList').html(receiptProductRows);

            // Llenar el total en el modal de factura
            $('#receiptTotalAmount').text(`₡${globalOrderData.totalAmount.toFixed(2)}`);

            // Mostrar el modal de factura
            $('#print-receipt').modal('show');
        });

        $('#confirmOrderButton').click(function () {
            // Obtener la información de los productos
            var ordenProductos = [];
            var totalAmount = 0;
            $('.product-list').each(function () {
                var categoriaId = $(this).data('categoriaid');
                var productoId = $(this).data('productoid');
                var productoNombre = $(this).data('nombre');
                var productoPrecio = parseFloat($(this).data('precio'));
                var productoTipo = $(this).data('tipo') || 'N/A';
                var productoSabores = $(this).data('sabores');
                if (productoSabores === 'undefined') {
                    var productoSabores = 'N/A';
                }
                var productoDescripcion = $(this).data('descripcion');
                if (productoDescripcion === 'undefined') {
                    productoDescripcion = 'N/A';
                }
                var productoCantidad = parseInt($(this).find('.qty-input').val());
                var productoTotal = productoPrecio * productoCantidad;
                totalAmount += productoTotal;
                var productoNota = $(this).find('.product-note').val() || 'N/A';

                ordenProductos.push({
                    idProducto: productoId,
                    idCategoria: categoriaId,
                    nombre: productoNombre,
                    precioUnitario: productoPrecio,
                    tipo: productoTipo,
                    descripcion: productoDescripcion,
                    sabores: productoSabores,
                    cantidad: productoCantidad,
                    total: productoTotal,
                    nota: productoNota
                });
            });

            // Obtener el método de pago seleccionado
            var selectedMethod = $('.payment-method .selected-method span').text();

            // Verificar si el pedido es express
            var isExpress = $('#express-order-checkbox').is(':checked');

            // Obtener la información del cliente
            var customerNombre = $('#customerNombreHidden').text() || $('#customerNameInput input[name="nombre"]').val() || 'N/A';
            var customerCorreo = $('#customerCorreoHidden').text() || 'N/A';
            var customerTelefono = $('#customerTelefonoHidden').text() || 'N/A';
            var customerDireccion = $('#customerDireccionHidden').text() || 'N/A';

            // Crear el objeto JSON con toda la información
            var orderData = {
                ordenProductos: ordenProductos,
                montoTotal: totalAmount,
                metodoPago: selectedMethod,
                express: isExpress,
                clienteOrden: {
                    nombre: customerNombre,
                    correo: customerCorreo,
                    telefono: customerTelefono,
                    direccion: customerDireccion
                }
            };
            // Verificar si el método de pago es con tarjeta
            if (selectedMethod.toLowerCase().includes('tarjeta')) {
                // Iniciar proceso de Stripe
                iniciarProcesoStripe(orderData);
            } else {
                // Procesar la orden normalmente
                procesarOrden(orderData);
            }
        });

            // Enviar la solicitud AJAX al controlador
        function iniciarProcesoStripe(orderData) {
            $.ajax({
                type: 'POST',
                url: '/OrdenPDV/CompletarOrden',
                contentType: 'application/json',
                data: JSON.stringify(orderData),
                success: function (response) {
                    if (response.success) {
                        stripeSessionId = response.sessionId;
                        $('#qrcode').html('<img src="https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=' + encodeURIComponent(response.stripeUrl) + '" alt="QR Code" />');
                        $('#detalle-orden').modal('hide'); 
                        $('#qrModal').modal('show');
                        startCheckingPaymentStatus();
                    } else {
                        showErrorMessage("Error al iniciar sesión de Stripe: " + response.message);
                    }
                },
                error: function (error) {
                    console.error(error);
                    showErrorMessage("Ocurrió un error al iniciar el proceso de pago con Stripe.");
                }
            });
        }

        function procesarOrden(orderData) {
            $.ajax({
                type: 'POST',
                url: '/OrdenPDV/CompletarOrden',
                contentType: 'application/json',
                data: JSON.stringify(orderData),
                success: function (response) {
                    if (response.success) {
                        showSuccessMessage(response.idOrden, response.nombre);
                    } else {
                        showErrorMessage(response.message);
                    }
                },
                error: function (error) {
                    console.error(error);
                    showErrorMessage("Ocurrió un error al procesar la orden.");
                }
            });
        }

        function startCheckingPaymentStatus() {
            let attempts = 0;
            let maxAttempts = 12;  // Intenta por 1 minuto (12 veces cada 5 segundos)
            let checkInterval = setInterval(function () {
                attempts++;
                $.ajax({
                    url: '/OrdenPDV/CheckStripePaymentStatus',
                    data: { sessionId: stripeSessionId },
                    success: function (response) {
                        if (response.isPaid) {
                            clearInterval(checkInterval);
                            $('#qrModal').modal('hide');
                            showSuccessMessage("Pago completado con Stripe");
                        } else if (attempts >= maxAttempts) {
                            clearInterval(checkInterval);
                            showErrorMessage("No se pudo completar el pago. Por favor, intenta de nuevo.");
                        }
                    },
                    error: function () {
                        if (attempts >= maxAttempts) {
                            clearInterval(checkInterval);
                            showErrorMessage("Error al verificar el estado del pago. Intenta más tarde.");
                        }
                    }
                });
            }, 5000);
        }

        function showSuccessMessage(idOrden, nombre) {
            Swal.fire({
                title: '¡Éxito!',
                text: "Orden completada exitosamente. Número de orden: " + idOrden + ", Cliente: " + nombre,
                icon: 'success',
                confirmButtonText: 'Aceptar'
            }).then(() => {
                $('#detalle-orden').modal('hide');
                $('#payment-completed').modal('show');
            });
        }

        function showErrorMessage(message) {
            Swal.fire({
                title: 'Error',
                text: message,
                icon: 'error',
                confirmButtonText: 'Aceptar'
            });
        }

  });



$(document).ready(function () {
    // Manejar el checkbox de pedido express

    $('#express-order-checkbox').change(function () {
        if ($(this).is(':checked')) {
            $('.customer-info.block-section').removeClass('hidden');
            $('#customerNameInput').closest('.input-blocks').addClass('hidden'); // Ocultar input
        } else {
            $('#customerNameInput').closest('.input-blocks').removeClass('hidden'); // Mostrar input
            $('.customer-info.block-section').addClass('hidden');
        }
    });

    // Manejar el envío del formulario del modal
    $('#create-customer-form').submit(function (event) {
        event.preventDefault(); // Prevenir el envío por defecto del formulario

        // Obtener los valores del formulario
        const nombre = $(this).find('input[name="nombre"]').val();
        const correo = $(this).find('input[name="correo"]').val();
        const telefono = $(this).find('input[name="telefono"]').val();
        const direccion = $(this).find('input[name="direccion"]').val();


        // Guardar la información en elementos ocultos para después usarlos
        $('#customerNombreHidden').text(nombre);
        $('#customerCorreoHidden').text(correo);
        $('#customerTelefonoHidden').text(telefono);
        $('#customerDireccionHidden').text(direccion);

        // Crear el HTML para mostrar la información del cliente solo si los campos no están vacíos
        let customerInfoHTML = '<div class="customer-details">';
        if (nombre) {
            customerInfoHTML += `<p><strong>Nombre del cliente:</strong> ${nombre}</p>`;
        }
        if (correo) {
            customerInfoHTML += `<p><strong>Correo:</strong> ${correo}</p>`;
        }
        if (telefono) {
            customerInfoHTML += `<p><strong>Teléfono:</strong> ${telefono}</p>`;
        }
        if (direccion) {
            customerInfoHTML += `<p><strong>Dirección:</strong> ${direccion}</p>`;
        }
 
        customerInfoHTML += '</div>';

        // Limpiar la información anterior del cliente
        $('.customer-info.block-section .customer-details').remove();

        // Añadir la nueva información del cliente al div .customer-info.block-section solo si hay información para mostrar
        if (customerInfoHTML !== '<div class="customer-details"></div>') {
            $('.customer-info.block-section').append(customerInfoHTML);
        }

        // Cerrar el modal
        $('#create').modal('hide');

        // Limpiar el formulario
        $('#create-customer-form')[0].reset();
    });

    $(document).ready(function () {
        $('.payment-method .item .default-cover a').click(function () {
            // Remover la clase 'selected-method' de todos los métodos de pago
            $('.payment-method .item .default-cover').removeClass('selected-method');

            // Añadir la clase 'selected-method' al contenedor seleccionado
            $(this).closest('.default-cover').addClass('selected-method');
        });
    });

    $(document).ready(function () {
        var phoneInput = $('input[name="telefono"]');
        phoneInput.val('+506 '); // Inicializar el campo con +506

        phoneInput.on('focus', function () {
            if (phoneInput.val() === '') {
                phoneInput.val('+506 ');
            }
        });

        phoneInput.on('input', function () {
            var currentValue = phoneInput.val();
            if (!currentValue.startsWith('+506 ')) {
                phoneInput.val('+506 ' + currentValue.replace(/[^0-9]/g, '').substring(0, 8));
            }
        });

        phoneInput.on('keydown', function (event) {
            // Permitir borrar
            if (event.key === "Backspace" || event.key === "Delete") {
                var currentValue = phoneInput.val();
                if (currentValue.length <= 5) {
                    event.preventDefault();
                }
            }
        });

        phoneInput.mask('+506 0000-0000', {
            onKeyPress: function (val, e, field, options) {
                field.mask('+506 0000-0000', options);
            }
        });
    });
   


});

});

$(document).ready(function () {
    $('#downloadReceiptButton').click(function () {
        // Seleccionar el contenido del modal de factura
        var element = document.getElementById('receipt-content');
        var downloadButton = document.getElementById('downloadReceiptButton');
        var closeButton = element.querySelector('.close');

        // Ocultar el botón de descarga y el ícono de cierre
        downloadButton.style.display = 'none';
        if (closeButton) {
            closeButton.style.display = 'none';
        }

        // Opciones para la generación del PDF
        var opt = {
            margin: [0.5, 0.5, 0.5, 0.5], // Márgenes en pulgadas
            filename: 'factura.pdf',
            image: { type: 'jpeg', quality: 0.98 },
            html2canvas: { scale: 2 },
            jsPDF: { unit: 'in', format: [5.5, 8.5], orientation: 'portrait' } // Formato personalizado
        };

        // Generar y descargar el PDF

        html2pdf().from(element).set(opt).save().then(function () {
            // Mostrar el botón de descarga y el ícono de cierre nuevamente
            downloadButton.style.display = 'block';
            if (closeButton) {
                closeButton.style.display = 'block';
            }
            // Refrescar la página
            location.reload();
        });
    });
});
