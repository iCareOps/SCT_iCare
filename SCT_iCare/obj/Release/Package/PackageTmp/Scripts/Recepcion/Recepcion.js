

$(function () {
    $(".buscar").click(function () {
        var url = "~/Controllers/Recepcion/Buscar";//"@Url.Action("Buscar", "Recepcion")";
        var dato = $("#buscador").val();
        var data = { dato: dato };

        $.post(url, data).done(function (data) {
            console.log(data);
            let miResultado = "";
            if ($.isEmptyObject(data)) {
                miResultado = "<h3>No se encontraron registros!!</h3>"
            }
            else {
                miResultado += "<h4 class=\"text-danger\">Se encontraron <b>" + data.length + "</b> registros:</h4><hr/>"
                for (let i = 0; i < data.length; i++) {

                    if (data[i].NoExpediente == null) {
                        miResultado +=
                            "<h5><b><mark style=\"background-color: #41F51A; padding:5px; border-radius: 10px\">ID: " + data[i].idPaciente + "</mark></b></h5>" +
                            "<p>Nombre: <b>" + data[i].Nombre + "</b></p>" +
                            "<p>Teléfono: <b>" + data[i].Telefono + "</b></p>" +
                            "<p>Email: <b>" + data[i].Email + "</b></p>" +
                            "<p>Folio: <b>" + data[i].Folio + "</b></p>" +
                            "<p>CURP: <b>" + data[i].CURP + "</b></p>" +
                            "<hr/>"
                    }
                    else {
                        miResultado +=
                            "<h5><b><mark style=\"background-color: #41F51A; padding:5px; border-radius: 10px\">ID: " + data[i].idPaciente + "</mark></b></h5>" +
                            "<p>Nombre: <b>" + data[i].Nombre + "</b></p>" +
                            "<p>Teléfono: <b>" + data[i].Telefono + "</b></p>" +
                            "<p>Email: <b>" + data[i].Email + "</b></p>" +
                            "<p>Folio: <b>" + data[i].Folio + "</b></p>" +
                            "<p>CURP: <b>" + data[i].CURP + "</b></p>" +
                            "<p>Tipo de Pago: <b>" + data[i].TipoPago + "</b></p>" +
                            "<p>Fecha Cita: <b>" + data[i].FechaCita + "</b></p>" +
                            "<p>No. Orden: <b>" + data[i].NoOrden + "</b></p>" +
                            "<p>Estatus Pago: <b>" + data[i].EstatusPago + "</b></p>" +
                            "<p>Tipo Licencia : <b>" + data[i].TipoLicencia + "</b></p>" +
                            "<p>No. Expediente: <b>" + data[i].NoExpediente + "</b></p>" +
                            "<p>Referencia: <b>" + data[i].Referencia + "</b></p>" +
                            "<p>Fecha Referencia: <b>" + data[i].FechaReferencia + "</b></p>" +
                            "<p>Sucursal: <b>" + data[i].Sucursal + "</b></p>" +
                            "<p>Doctor: <b>" + data[i].Doctor + "</b></p>" +
                            "<p>Tipo Trámite: <b>" + data[i].TipoTramite + "</b></p>" +
                            "<hr/>"
                    }
                }
            }

            $('#exampleModal5').modal('show');
            document.getElementById("cuerpo").innerHTML = miResultado;

        }).fail().always(function () {

        });
    });
});