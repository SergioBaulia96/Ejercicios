window.onload = ListadoLugares();

function ListadoLugares(){
    let personaID = document.getElementById("PersonaID").value;

    $.ajax({
        // la URL para la petición
        url: '../../Lugares/ListadoLugares',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { 
            personaID: personaID
         },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (tipoDeLugares) {

            $("#ModalLugar").modal("hide");
            LimpiarModal();
            //$("#tbody-tipoejercicios").empty();
            let contenidoTabla = ``;

            $.each(tipoDeLugares, function (index, tipoDeLugar) {  
                
                contenidoTabla += `
                <tr>
                    <td>${tipoDeLugar.nombre}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${tipoDeLugar.lugarID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger" onclick="EliminarRegistro(${tipoDeLugar.lugarID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
            `;
            });

            document.getElementById("tbody-lugares").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}

function LimpiarModal(){
    document.getElementById("LugarID").value = 0;
    document.getElementById("nombre").value = "";
}

function NuevoRegistro(){
    $("#ModalTitulo").text("Nuevo Tipo de Lugar");
}

function AbrirModalEditar(lugarID){
    
    $.ajax({
        // la URL para la petición
        url: '../../Lugares/ListadoLugares',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { id: lugarID},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (tipoDeLugares) {
            let tipoDeLugar = tipoDeLugares[0];

            document.getElementById("LugarID").value = lugarID;
            $("#ModalTitulo").text("Editar Lugar");
            document.getElementById("nombre").value = tipoDeLugar.nombre;
            $("#ModalLugar").modal("show");
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al consultar el registro para ser modificado.');
        }
    });
}

function GuardarRegistro() {
    let lugarID = document.getElementById("LugarID").value;
    let nombre = document.getElementById("nombre").value;
    let personaID = document.getElementById("PersonaID").value;

            $.ajax({
                url: '../../Lugares/GuardarLugares',
                data: { lugarID: lugarID, nombre: nombre, personaID: personaID},
                type: 'POST',
                dataType: 'json',
                success: function (resultado) {
                    if (resultado != "") {
                        alert(resultado);
                    }
                    ListadoLugares();
                },
                error: function (xhr, status) {
                    console.log('Disculpe, existió un problema al guardar el registro');
                }
            });
}


function EliminarRegistro(lugarID) {
    Swal.fire({
        title: "¿Esta seguro de eliminar?",
        text: "No se podra revertir!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '../../Lugares/EliminarLugares',
                data: { lugarID: lugarID },
                type: 'POST',
                dataType: 'json',
                success: function (resultado) {
                    ListadoLugares();
                    Swal.fire("Eliminado!", "Se elimino correctamente.", "success");
                },
                error: function (xhr, status) {
                    console.log('Disculpe, existió un problema al eliminar el registro.');
                }
            });
        }
    });
}
