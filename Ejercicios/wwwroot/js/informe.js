window.onload = ListadoInforme();

function ListadoInforme() {
    let buscarInicio = document.getElementById("BuscarInicio").value;
    let buscarFin = document.getElementById("BuscarFin").value;
    let tipoEjercicioBuscar = document.getElementById("TipoEjercicioBuscar").value;

    $.ajax({
        url: '../../EjercicioFisicos/ListadoInforme',
        data: {
            buscarInicio : buscarInicio,
            buscarFin : buscarFin,
            tipoEjercicioBuscar : tipoEjercicioBuscar
        },
        type: 'POST',
        dataType: 'json',
        success: function (tiposEjerciciosMostrar) {

            let contenidoTabla = ``;

            $.each(tiposEjerciciosMostrar, function (index, tipoEjercicio) {

                contenidoTabla += `
                <tr>
                    <td>${tipoEjercicio.descripcion}</td>
                    <td colspan="7"></td>
                </tr>
             `;

                $.each(tipoEjercicio.listadoLugares, function (index, lugar) {
                    contenidoTabla += `
                        <tr>
                            <td></td>
                            <td>${lugar.nombreLugar}</td>
                            <td colspan="6"></td>
                        </tr>
                    `;

                    $.each(lugar.listadoEjercicios, function (index, ejercicios) {
                        contenidoTabla += `
                            <tr>
                                <td  colspan="2"></td>
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

            document.getElementById("tbody-ejerciciosportipo").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}