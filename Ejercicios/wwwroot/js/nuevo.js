
window.onload = ListadoInforme();

function ListadoInforme() {
    let buscarInicioActividad = document.getElementById("BuscarInicioActividad").value;
    let buscarFinActividad = document.getElementById("BuscarFinActividad").value;
    $.ajax({
        // la URL para la petición
        url: '../../EjercicioFisicos/ListadoInforme',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { BuscarInicioActividad: buscarInicioActividad,
            BuscarFinActividad: buscarFinActividad,
         },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (ejerciciosFisicosMostrar) {


            let contenidoTabla = ``;

            $.each(ejerciciosFisicosMostrar, function (index, ejercicio) {  
                

                contenidoTabla += `
                <tr class="table-success">
                    <td class="anchoCelda">${ejercicio.descripcion}</td>
                    <td class="text-center anchoCelda"></td>
                    <td class="anchoCelda"></td>
                    <td class="text-center anchoCelda"></td>
                    <td class="anchoCelda"></td>
                    <td class="text-center anchoCelda anchoBotones"></td>
                    <td class="text-center anchoBotones"></td>
                </tr>
             `;

                $.each(tipoEjercicio.listadoEjercicios, function (index, ejercicio) {  
                    contenidoTabla += `
                    <tr>
                        <td class="anchoCelda"></td>
                        <td class="text-center anchoCelda">${ejercicio.inicioString}</td>
                        <td class="text-center anchoCelda">${ejercicio.finString}</td>
                        <td class="text-center anchoCelda">${ejercicio.intervaloEjercicio}</td>
                        <td class="anchoCelda">${ejercicio.estadoEmocionalInicio}</td>                  
                        <td class="anchoCelda">${ejercicio.estadoEmocionalFin}</td>
                        <td class="text-center anchoCelda anchoBotones">${ejercicio.observaciones}</td>
                    </tr>
                 `;
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
