window.onload = ListadosInformeEventos();

function ListadosInformeEventos() {
    let buscarInicioEvento = document.getElementById("BuscarInicioEvento").value;
    let buscarFinEvento = document.getElementById("BuscarFinEvento").value;

    $.ajax({
        url: '../../EjercicioFisicos/ListadosInformeEventos',
        data: {
            buscarInicioEvento : buscarInicioEvento,
            buscarFinEvento : buscarFinEvento
        },
        type: 'POST',
        dataType: 'json',
        success: function (eventosDeportivosMostrar) {

            let contenidoTabla = ``;

            $.each(eventosDeportivosMostrar, function (index, eventoDeportivo) {

                contenidoTabla += `
                <tr>
                    <td>${eventoDeportivo.nombre}</td>
                    <td colspan="8"></td>
                </tr>
             `;

                $.each(eventoDeportivo.listadoLugares, function (index, lugar) {
                    contenidoTabla += `
                        <tr>
                            <td></td>
                            <td>${lugar.nombreLugar}</td>
                            <td colspan="7"></td>
                        </tr>
                    `;

                    $.each(lugar.listadoTipoEjercicios, function (index, tipoEjercicio) {
                        contenidoTabla += `
                            <tr>
                                <td></td>
                                <td></td>
                                <td>${tipoEjercicio.descripcion}</td>
                                <td colspan="6"></td>
                            </tr>
                        `;

                    $.each(tipoEjercicio.listadoEjercicios, function (index, ejercicios) {
                        contenidoTabla += `
                            <tr>
                                <td  colspan="3"></td>
                                <td>${ejercicios.inicioString}</td>
                                <td>${ejercicios.finString}</td>
                                <td>${ejercicios.intervaloEjercicio} - min</td>
                                <td>${ejercicios.estadoEmocionalInicio}</td>
                                <td>${ejercicios.estadoEmocionalFin}</td>
                                <td>${ejercicios.observaciones}</td>
                            </tr>
                        `;
                    });

                });

            });

            });

            document.getElementById("tbody-infomeEventos").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}