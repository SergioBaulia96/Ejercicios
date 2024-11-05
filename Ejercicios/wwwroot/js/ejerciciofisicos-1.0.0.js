window.onload = ListadoEjerciciosFisicos();

function ListadoEjerciciosFisicos(){
    let personaID = document.getElementById("PersonaID").value;

    $.ajax({
        url: '../../EjercicioFisicos/ListadoEjerciciosFisicos',
        data: {
            personaID: personaID
        },
        type: 'POST',
        datatype: 'json',
        success: function(ejerciciosFisicoMostrar){
            $("#ModalEjerciciosFisicos").modal("hide");
            LimpiarModal();
            //console.log("Ejecuta funcion limpiar modal")
            let contenidoTabla = ``;

            $.each(ejerciciosFisicoMostrar, function(index, ejercicioFisicoMostrar){

                contenidoTabla += `
                <tr>
                    <td>${ejercicioFisicoMostrar.descripcion}</td>
                    <td>${ejercicioFisicoMostrar.nombreLugar}</td>
                    <td>${ejercicioFisicoMostrar.nombreEvento}</td>
                    <td>${ejercicioFisicoMostrar.inicioString}</td>
                    <td>${ejercicioFisicoMostrar.finString}</td>
                    <td>${ejercicioFisicoMostrar.estadoEmocionalInicio}</td>
                    <td>${ejercicioFisicoMostrar.estadoEmocionalFin}</td>
                    <td>${ejercicioFisicoMostrar.observaciones}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success" onclick="AbrirEditarEjercicioFisico(${ejercicioFisicoMostrar.ejercicioFisicoID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger" onclick="EliminarEjercicioFisico(${ejercicioFisicoMostrar.ejercicioFisicoID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
                
            });
            document.getElementById("tbody-ejerciciosfisicos").innerHTML = contenidoTabla;
        },
        error: function (xhr, status){
            alert('Disculpe, existio un problema al deshabilitar');
        }
    });
}

function LimpiarModal(){
    document.getElementById("EjercicioFisicoID").value = 0;
    document.getElementById("TipoEjercicioID").value = 0;
    document.getElementById("LugarID").value = 0;
    document.getElementById("EventoDeportivoID").value = 0;
    document.getElementById("EstadoEmocionalInicio").value = 0;
    document.getElementById("EstadoEmocionalFin").value = 0;

}

function NuevoRegistroEjercicioFisico(){
    $("#ModalTituloEjerciciosFisicos").text("Nuevo Ejercicio Fisico");
}

function GuardarRegistro(){
    let ejercicioFisicoID = document.getElementById("EjercicioFisicoID").value;
    let tipoEjercicioID = document.getElementById("TipoEjercicioID").value;
    let lugarID = document.getElementById("LugarID").value;
    let eventoDeportivoID = document.getElementById("EventoDeportivoID").value;
    let inicio = document.getElementById("FechaInicio").value;
    let fin = document.getElementById("FechaFin").value;
    let estadoEmocionalInicio = document.getElementById("EstadoEmocionalInicio").value;
    let estadoEmocionalFin = document.getElementById("EstadoEmocionalFin").value;
    let observaciones = document.getElementById("Observaciones").value;

    $.ajax({
        url: '../../EjercicioFisicos/GuardarEjercicioFisico',
        data: { EjercicioFisicoID: ejercicioFisicoID,
            TipoEjercicioID: tipoEjercicioID,
            LugarID : lugarID,
            EventoDeportivoID : eventoDeportivoID,
            Inicio: inicio,
            Fin: fin,
            EstadoEmocionalInicio: estadoEmocionalInicio,
            EstadoEmocionalFin: estadoEmocionalFin,
            Observaciones: observaciones,
        },
        type: 'POST',
        datatype: 'json',
        success: function (resultado) {
            if(resultado != ""){
                alert(resultado);
            }
            Swal.fire({
                position: "center",
                icon: "success",
                title: "Registro guardado correctamente!",
                showConfirmButton: false,
                timer: 1000
            });
            ListadoEjerciciosFisicos();
        },
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al guardar el registro');
        }
    });
}

function AbrirEditarEjercicioFisico(EjercicioFisicoID){
    $.ajax({
        url:'../../EjercicioFisicos/TraerEjerciciosFisicosAlModal',
        data: {EjercicioFisicoID: EjercicioFisicoID},
        type: 'POST',
        datatype: 'json',
        success: function (ejercicioFisicoPorID){
            let ejercicioFisico = ejercicioFisicoPorID[0];

            document.getElementById("EjercicioFisicoID").value = EjercicioFisicoID;
            $("#ModalTituloEjerciciosFisicos").text("Editar Ejercicio Fisico");
            document.getElementById("TipoEjercicioID").value = ejercicioFisico.tipoEjercicioID,
            document.getElementById("LugarID").value = ejercicioFisico.lugarID,
            document.getElementById("EventoDeportivoID").value = ejercicioFisico.eventoDeportivoID,
            document.getElementById("FechaInicio").value = ejercicioFisico.inicio,
            document.getElementById("FechaFin").value = ejercicioFisico.fin,
            document.getElementById("EstadoEmocionalInicio").value = ejercicioFisico.estadoEmocionalInicio,
            document.getElementById("EstadoEmocionalFin").value = ejercicioFisico.estadoEmocionalFin,
            document.getElementById("Observaciones").value = ejercicioFisico.observaciones;

            $("#ModalEjerciciosFisicos").modal("show");
        },

        error: function (xhr, status){
            console.log('Disculpe, exitio un problema al editar el registro.');
        }
    });
}

function EliminarEjercicioFisico(EjercicioFisicoID){
    Swal.fire({
        title: "Esta seguro que quiere eliminar el registro?",
        text: "No podrás recuperarlo!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar!",
        cancelButtonText: "Cancelar"
    }).then((result) =>{
        if (result.isConfirmed){
            $.ajax({
                url: '../../EjercicioFisicos/EliminarEjercicioFisico',
                data: {ejercicioFisicoID: EjercicioFisicoID},
                type: 'POST',
                datatype: 'json',
                success: function(resultado){
                    Swal.fire({
                        title: "Eliminado!",
                        text: "El registro se elimino correctamente",
                        icon: "success",
                        confirmButtonColor: "#000"
                    });
                    ListadoEjerciciosFisicos();
                },
                error: function(xhr, status){
                    console.log('Disculpe, existio un preoblema al eliminar el registro.');
                }
            });
        }
    });
}