window.onload = ListadoEventosDeportivos();

function ListadoEventosDeportivos()
{
    $.ajax({
        url: '../../EventosDeportivos/ListadoEventosDeportivos',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function(listadoEventosDeportivos){
            $("#ModalEvento").modal("hide");
            LimpiarModal();
            
            let tabla = ``

            $.each(listadoEventosDeportivos, function(index, eventos){

                tabla += `
                <tr>
                    <td>${eventos.nombre}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success btn-sm" onclick="ModalEditar(${eventos.eventoDeportivoID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="ValidarEliminacion(${eventos.eventoDeportivoID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("tbody-eventosdeportivos").innerHTML = tabla;                                
        },
        error: function(xhr, status){
            console.log('Problemas al cargar la tabla');
        }
    });
}

function LimpiarModal(){
    document.getElementById("EventoDeportivoID").value = 0;
    document.getElementById("Nombre").value = "";
}

function NuevoEventoDeportivo(){
    $("#ModalTitulo").text("Nuevo Evento");
}

function GuardarEventoDeportivo(){
    let eventoDeportivoID = document.getElementById("EventoDeportivoID").value;
    let nombre = document.getElementById("Nombre").value;



    $.ajax({
        url: '../../EventosDeportivos/GuardarEventoDeportivo',
        data: {
            eventoDeportivoID : eventoDeportivoID,
            nombre : nombre
        },
        type: 'POST',
        dataType: 'json',
        success: function(resultado){
            if(resultado != "") {
                alert(resultado)
            }
            ListadoEventosDeportivos();
        },
        error: function(xhr, status){
            console.log('Problemas al guardar Club');
        },
    });
}

function ModalEditar(EventoDeportivoID){
    $.ajax({
        url: '../../EventosDeportivos/ListadoEventosDeportivos',
        data: { eventoDeportivoID : EventoDeportivoID },
        type: 'POST',
        dataType: 'json',
        success: function(listadoEventosDeportivos){
            listadoEventoDeportivo = listadoEventosDeportivos[0];
            
            document.getElementById("EventoDeportivoID").value = EventoDeportivoID
            $("#ModalTitulo").text("Editar Evento");
            document.getElementById("Nombre").value = listadoEventoDeportivo.nombre;
            $("#ModalEvento").modal("show");
        },
        error: function(xhr, status){
            console.log('Problemas al cargar Club');
        }
    });
}

function ValidarEliminacion(EventoDeportivoID)
{
    var elimina = confirm("¿Esta seguro que desea eliminar?");
    if(elimina == true)
        {
            EliminarEventoDeportivo(EventoDeportivoID);
        }
}

function EliminarEventoDeportivo(EventoDeportivoID){
    $.ajax({
        url: '../../EventosDeportivos/EliminarEventoDeportivo',
        data: { eventoDeportivoID: EventoDeportivoID },
        type: 'POST',
        dataType: 'json',
        success: function(resultado){
            if (resultado != "Evento eliminado correctamente.") {
                alert(resultado);  // Muestra el mensaje si no se puede eliminar
            } else {
                ListadoEventosDeportivos();  // Actualiza la tabla de localidades
            }
        },
        error: function(xhr, status){
            console.log('Problemas al eliminar el evento');
        }
    });
}