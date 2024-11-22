window.onload = InformeCuentas();

function InformeCuentas() {
    $.ajax({
        url: '../../Admin/InformeCuentas',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function (vistaCuentas) {

            let contenidoTabla = ``;

            $.each(vistaCuentas, function (index, cuenta) {

                contenidoTabla += `
                <tr>
                    <td class="text-center align-middle">${cuenta.nombreCompleto}</td>
                    <td class="text-center align-middle">${cuenta.emailPersona}</td>
                    <td class="text-center align-middle">${cuenta.fechaNacimiento}
                    <td class="text-center align-middle">${cuenta.genero}</td>
                    <td class="text-center align-middle">${cuenta.peso} kg</td>
                    <td class="text-center align-middle">${(cuenta.altura)} cm</td>
                    <td class="text-center align-middle">${cuenta.rol}</td>
                </tr>
            `;

            });

            document.getElementById("tbody-administradores").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}
