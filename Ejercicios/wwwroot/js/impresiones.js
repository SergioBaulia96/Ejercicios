function ImprimirTablaUsuarios() {
    var doc = new jsPDF('landscape', 'mm', [210, 297]);

    var totalPagesExp = "{total_pages_count_string}"
    var pageContent = function (data) {

        var pageHeight = doc.internal.pageSize.height || doc.internal.pageSize.getHeight();
        var pageWidth = doc.internal.pageSize.width || doc.internal.pageSize.getWidth();

        //FOOTER
        var str = "Página " + doc.internal.getNumberOfPages();
        // Total page number plugin only available in jspdf v1.0+
        if (typeof doc.putTotalPages == 'function') {
            str = str + " de " + totalPagesExp;
        }

        //ESTABLECER ANCHO DE LINEA
        doc.setLineWidth(8);

        //ESTABLECER COLOR DE LINEA
        doc.setDrawColor(238, 238, 238);

        //DIBUJAR UNA LINEA HORIZONTAL
        doc.line(14, pageHeight - 11, pageWidth - 14, pageHeight - 11);

        //ESTABLECER TAMAÑO DE FUENTE
        doc.setFontSize(10);

        //ESTABLECER ESTILO DE FUENTE A NEGRITA
        doc.setFontStyle('bold');

        //AGREGAR TEXTO AL PIE DE PAGINA
        doc.text(str, 17, pageHeight - 10);
    };

    // Add title and date to the first page
    doc.setFontSize(18);
    doc.setFontStyle('bold');
    doc.text('Listado de Usuarios', 14, 22);

    var element = document.getElementById("imprimir-tabla");

    //CONVERTIR TABLA HTML A JSON
    var res = doc.autoTableHtmlToJson(element);

    doc.autoTable(res.columns, res.data, {
        addPageContent: pageContent,
        theme: 'grid',
        fillColor: [0, 0, 0], 
        columnStyles: {
            0: {
                cellWidth: 'auto',
                fontSize: 8,
                fillColor: [255, 255, 255]
            },

            1: {
                fontSize: 7,
                overflow: 'hidden',
                fillColor: [255, 255, 255]
            },

            2: {
                fontSize: 7,
                fillColor: [255, 255, 255]
            },

            3: {
                fontSize: 7,
                fillColor: [255, 255, 255]
            },

            4: {
                fontSize: 7,
                fillColor: [255, 255, 255]
            },

            5: {
                fontSize: 7,
                fillColor: [255, 255, 255]
            },
            6: {
                fontSize: 7,
                fillColor: [255, 255, 255]
            },

        },
        margin: { top: 30 } // Adjust top margin for title
    });

    // ESTO SE LLAMA ANTES DE ABRIR EL PDF PARA QUE MUESTRE EN EL PDF EL NRO TOTAL DE PAGINAS. ACA CALCULA EL TOTAL DE PAGINAS.
    if (typeof doc.putTotalPages === 'function') {
        doc.putTotalPages(totalPagesExp);
    }

    //doc.save('InformeSistema.pdf')

    var string = doc.output('datauristring');
    var iframe = "<iframe width='100%' height='100%' src='" + string + "'></iframe>"

    var x = window.open();
    x.document.open();
    x.document.write(iframe);
    x.document.close();
}