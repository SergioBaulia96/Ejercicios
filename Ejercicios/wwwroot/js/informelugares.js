window.onload = ListadoInformes();

function ListadoInformes() {
    let buscarInicio = document.getElementById("BuscarInicio").value;
    let buscarFin = document.getElementById("BuscarFin").value;

    $.ajax({
        url: '../../EjercicioFisicos/ListadoInformes',
        data: {
            id: null,
            BuscarInicio: buscarInicio,
            BuscarFin: buscarFin,
        },
        type: 'POST',
        dataType: 'json',
        success: function (ejerciciosFisicosMostrar){
            let contenidoTabla = ``;
            let agrupadoPorTipo = {};

            $.each(ejerciciosFisicosMostrar, function (index, ejercicio) {
                // Agrupar por TipoEjercicioDescripcion
                if (!agrupadoPorTipo[ejercicio.nombreLugar]) {
                    agrupadoPorTipo[ejercicio.nombreLugar] = [];
                }
                agrupadoPorTipo[ejercicio.nombreLugar].push(ejercicio);
            });

            // Construir el contenido de la tabla
            for (let tipoLugar in agrupadoPorTipo) {
                contenidoTabla += `
                <tr>
                    <td>${tipoLugar}</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                `;

                $.each(agrupadoPorTipo[tipoLugar], function (index, ejercicio) {
                    contenidoTabla += `
                    <tr>
                        <td></td>
                        <td>${ejercicio.descripcion}</td>
                        <td>${ejercicio.inicioString}</td>
                        <td>${ejercicio.finString}</td>
                        <td>${ejercicio.intervaloEjercicio} - min</td>
                        <td>${ejercicio.observaciones}</td>
                    </tr>
                    `
                });
        }
        document.getElementById("tbody-lugarportipo").innerHTML = contenidoTabla;
    },
    error: function (xhr, status) {
        alert('Disculpe, existió un problema al procesar la solicitud.');
    }
    });
}























// window.onload = ListadoInformeLugar();

// function ListadoInformeLugar() {
//     let buscarInicio = document.getElementById("BuscarInicio").value;
//     let buscarFin = document.getElementById("BuscarFin").value;
//     $.ajax({
//         // la URL para la petición
//         url: '../../EjerciciosFisicos/MostrarLugarPorTipo',
//         // la información a enviar
//         // (también es posible utilizar una cadena de datos)
//         data: {
//             BuscarInicio: buscarInicio,
//             BuscarFin: buscarFin
//         },
//         // especifica si será una petición POST o GET
//         type: 'POST',
//         // el tipo de información que se espera de respuesta
//         dataType: 'json',
//         // código a ejecutar si la petición es satisfactoria;
//         // la respuesta es pasada como argumento a la función
//         success: function (tiposLugaresMostrar) {


//             let contenidoTabla = ``;

//             $.each(tiposLugaresMostrar, function (index, lugar) {


//                 contenidoTabla += `
//                 <tr>
//                     <td>${lugar.nombre}</td>
//                     <td></td>
//                     <td></td>
//                     <td></td>
//                     <td></td>
//                     <td></td>
//                     <td></td>
//                 </tr>
//             `;

//                 $.each(lugar.listadoEjercicios, function (index, ejercicio) {
//                     contenidoTabla += `
//                     <tr>
//                         <td></td>
//                         <td></td>
//                         <td>${ejercicio.inicioString}</td>
//                         <td>${ejercicio.finString}</td>
//                         <td>${ejercicio.intervaloEjercicio} - min</td>
//                         <td>${ejercicio.observaciones}</td>
//                     </tr>
//                 `;
//                 });


//             });

//             document.getElementById("tbody-lugarportipo").innerHTML = contenidoTabla;

//         },

//         // código a ejecutar si la petición falla;
//         // son pasados como argumentos a la función
//         // el objeto de la petición en crudo y código de estatus de la petición
//         error: function (xhr, status) {
//             console.log('Disculpe, existió un problema al cargar el listado');
//         }
//     });
// }
