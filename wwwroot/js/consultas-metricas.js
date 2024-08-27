$(document).ready(function () {
    $.ajax({
        url: '/Metricas/ConsultarConteoVentasPorSemana',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response) {
                var numeroVentas = response.numeroVentas;
                var countersElement = $('.total-sales');
                countersElement.attr('data-count', numeroVentas);
                animateCounter(countersElement, numeroVentas);
            } else {
                $('.total-sales').text('No hay datos disponibles');
            }
        },
        error: function (xhr, status, error) {
            console.error("Error en la llamada AJAX:", error);
            $('.total-sales').text('Error al cargar');
        }
    });

    // Obtener y actualizar ventas por día
    $.ajax({
        url: '/Metricas/ConsultarConteoVentasPorDia',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response) {
                var numeroVentas = response.numeroVentas;
                var countersElement = $('.daily-sales');
                countersElement.attr('data-count', numeroVentas);
                animateCounter(countersElement, numeroVentas);
            } else {
                $('.daily-sales').text('No hay datos disponibles');
            }
        },
        error: function (xhr, status, error) {
            console.error("Error en la llamada AJAX:", error);
            $('.daily-sales').text('Error al cargar');
        }
    });

    // Obtener y actualizar ganancia semanal
    $.ajax({
        url: '/Metricas/ConsultarConteoVentasPorSemana',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response) {
                var montoTotalVentas = response.montoTotalVentas;
                var countersElement = $('.weekly-earnings');
                countersElement.attr('data-count', formatCurrency(montoTotalVentas))
                animateCounter(countersElement, montoTotalVentas, true);
            } else {
                $('.weekly-earnings').text('No hay datos disponibles');
            }
        },
        error: function (xhr, status, error) {
            console.error("Error en la llamada AJAX:", error);
            $('.weekly-earnings').text('Error al cargar');
        }
    });

    $(document).ready(function () {
        $.ajax({
            url: '/Metricas/ConsultarTransaccionesRecientes', // Cambia a la URL de tu controlador
            type: 'GET',
            success: function (data) {
                var tbody = $('.recent-transactions tbody');
                tbody.empty(); // Limpiar la tabla antes de añadir nuevos datos

                data.forEach(function (orden, index) {
                    var row = `
                        <tr>
                            <td>${index + 1}</td>
                            <td>
                                <div class="product-info">
                                    <a href="#" class="product-img"></a>
                                    <div class="info">
                                        <a href="#">Orden #${orden.idOrden}</a>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <span class="d-block head-text">${orden.metodoPago}</span>
                                <span class="text-blue">#${orden.idOrden}</span>
                            </td>
                            <td><span class="badge background-less border-success">${orden.estado}</span></td>
                            <td>${orden.total.toLocaleString('es-CR', { style: 'currency', currency: 'CRC' })}</td>
                        </tr>
                    `;
                    tbody.append(row);
                });
            },
            error: function (error) {
                console.error('Error al obtener los datos:', error);
            }
        });
    });

    function animateCounter($element, countTo) {
        $element.each(function () {
            var $this = $(this);
            $({ countNum: $this.text() }).animate({
                countNum: countTo
            }, {
                duration: 2000,
                easing: 'linear',
                step: function () {
                    $this.text(Math.floor(this.countNum));
                },
                complete: function () {
                    $this.text(this.countNum.toLocaleString());
                }
            });
        });
    }
});

$(document).ready(function () {
    $.ajax({
        url: '/Metricas/ConsultarTotalVentasProductoPorDia',
        method: 'GET',
        success: function (data) {
            renderTable(data);
        },
        error: function (error) {
            console.error('Error fetching data:', error);
        }
    });
});

function renderTable(data) {
    // Selecciona el cuerpo de la tabla
    var tableBody = $('.best-seller tbody');
    tableBody.empty(); // Limpia cualquier contenido existente

    // Itera sobre la respuesta y construye las filas de la tabla
    data.forEach(function (item) {
        var row = `<tr>
                        <td>
                            <div class="product-info">
                                <div class="info">
                                    <a href="">${item.nombreProducto}</a>
                                    <p class="dull-text">₡${formatCurrency(item.precio)}</p>
                                </div>
                            </div>
                        </td>
                        <td>
                            <p class="head-text">Ventas</p>
                            ${item.cantidadTotal}
                        </td>
                    </tr>`;
        tableBody.append(row);
    });
}

function formatCurrency(value) {
    return parseFloat(value).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
}

$.ajax({
    url: '/Metricas/ConsultarGanancias', // URL del controlador
    type: 'GET',
    dataType: 'json',
    success: function (response) {
        if (response && response.porcentajeCambio !== undefined) {
            var porcentajeCambio = response.porcentajeCambio;

            // Formatear el porcentaje a un formato con 0 decimales y agregar el símbolo de porcentaje
            var formattedPorcentaje = porcentajeCambio.toFixed(0) + '%';

            // Modificar el contenido del <p> para incluir el porcentaje dinámicamente
            $('.sales-range').html(`
                <span class="text-success">
                    <i data-feather="chevron-up" class="feather-16"></i>${formattedPorcentaje}&nbsp;
                </span>Aumento en comparación con la semana pasada
            `);
        } else {
            // Mostrar 0% y un mensaje indicando que no hay registros disponibles
            $('.sales-range').html(`
                <span class="text-danger">
                    <i data-feather="chevron-down" class="feather-16"></i>0%&nbsp;
                </span>No hay registros disponibles
            `);
        }

        // Actualizar los íconos de feather (si es necesario)
        feather.replace();
    },
    error: function (xhr, status, error) {
        console.error("Error en la llamada AJAX:", error);
        $('.sales-range').html(`
            <span class="text-danger">
                <i data-feather="chevron-down" class="feather-16"></i>0%&nbsp;
            </span>Error al cargar
        `);
    }
});

$(document).ready(function () {
    // Realizar la llamada AJAX para obtener los datos
    $.ajax({
        url: '/Metricas/ConsultarRestanteLotes', // Cambia esto por la URL de tu API
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            // Procesar los datos recibidos
            var ids = [];
            var valoresTotales = [];
            var valoresRestantes = [];

            response.forEach(function (item) {
                ids.push('Lote ' + item.idLote);
                valoresTotales.push(item.valorTotal);
                valoresRestantes.push(item.valorRestante);
            });

            // Configurar el gráfico de ApexCharts
            var sCol = {
                chart: {
                    height: 350,
                    type: 'bar',
                    toolbar: {
                        show: false,
                    }
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '55%',
                        endingShape: 'rounded'
                    },
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    show: true,
                    width: 2,
                    colors: ['transparent']
                },
                series: [{
                    name: 'Valor Total',
                    data: valoresTotales
                }, {
                    name: 'Valor Restante',
                    data: valoresRestantes
                }],
                xaxis: {
                    categories: ids,
                },
                yaxis: {
                    title: {
                        text: 'Insumos'
                    }
                },
                fill: {
                    opacity: 1
                },
                tooltip: {
                    y: {
                        formatter: function (val) {
                            return "Insumos " + val;
                        }
                    }
                }
            };

            // Renderizar el gráfico
            var chart = new ApexCharts(document.querySelector("#s-col"), sCol);
            chart.render();
        },
        error: function (xhr, status, error) {
            console.error("Error al obtener los datos:", error);
        }
    });
});