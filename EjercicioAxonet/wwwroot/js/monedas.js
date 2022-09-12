var nuevaMoneda;

OpenModalMoneda = (nuevo, monedaId) => {
      $('#modalMoneda').modal('show');
      var url = $('.OpenModalMonedaClass').data('request-url');
      nuevaMoneda = nuevo;
      LimpiarFormMoneda();

      if (nuevo) {
            $('#MonedaModalLabel').text('Nuevo Moneda');
      }
      else {
            $('#MonedaModalLabel').text('Editar Moneda');
            BuscarMoneda(monedaId, url);
      }
}

GuardarMoneda = () => {
      var url = nuevaMoneda ? $('.nuevoElemento').data('request-url') : $('.editarElemento').data('request-url');
      var formData = new FormData();

      formData.append('MonedaId', $('#idMoneda').val());
      formData.append('Moneda', $('#moneda').val());
      formData.append('MonedaAbreviatura', $('#abreviatura').val());

      Swal.fire({
            title: '¿Estas seguro de guardar el registro?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Guardar',
            cancelButtonText: 'Cancelar'
      }).then((result) => {
            if (result.isConfirmed) {
                  $.ajax({
                        url: url,
                        type: 'POST',
                        dataType: 'json',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                              $('#modalMoneda').modal('hide');
                              if (response.Code == 0 && response.HttpStatusCode == 200) {
                                    Swal.fire(
                                          '¡Guardado!',
                                          'Tu registro ha sido guardado correctamente.',
                                          'success'
                                    );
                                    if (nuevaMoneda && response.Result != null) {
                                          var icono = '<i class="fas fa-coins"></i>';
                                          var opciones = `
                                                <a href="#" class="OpenModalMonedaClass" title="Editar" data-request-url=" `+ urlMon + `" onclick="OpenModalMoneda(false,  ` + response.Result.MonedaId + `)"><i class="far fa-edit"></i></a>
                                                &nbsp
                                                <a href="#" class="EliminarMonedaClass" title="Eliminar" data-request-url=" `+ urlDelMon + `" onclick="EliminarMoneda(` + response.Result.MonedaId + `)"><i class="far fa-trash-alt"></i></a>
                                    `;
                                          var rowNode = tablaMoneda.row.add([icono, response.Result.Moneda, response.Result.MonedaAbreviatura, opciones]).draw().node();
                                          $(rowNode).attr('id', response.Result.MonedaId);
                                    }
                                    else if (!nuevaMoneda && response.Result != null) {
                                          var columnas = $('#tablaMoneda tbody tr#' + response.Result.MonedaId + '').find('td');
                                          var icono = `<i class="fas fa-users"></i>`;
                                          columnas.eq(0).html(icono);
                                          columnas.eq(1).text(response.Result.Moneda);
                                          columnas.eq(2).text(response.Result.MonedaAbreviatura);
                                    }
                              }
                              else {
                                    Swal.fire(
                                          '¡Opss!',
                                          'Al parecer hubo un detalle en el guardado.Vuelve a intentar más tarde o comunícate con el depto. de TI. ERROR: ' + response.Message,
                                          'warning'
                                    );
                              }
                        }
                  });
            }
      });
}

EliminarMoneda = (monenaId) => {
      Swal.fire({
            title: '¿Estas seguro de eliminar este registro?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Borrar',
            cancelButtonText: 'Cancelar'
      }).then((result) => {
            if (result.isConfirmed) {
                  var url = $('.EliminarMonedaClass').data('request-url');
                  $.ajax({
                        url: url,
                        type: 'POST',
                        dataType: 'json',
                        data: { id: monenaId },
                        success: function (response) {
                              if (response.Code == 0 && response.HttpStatusCode == 200) {
                                    var rowMon = tablaMoneda.row($('#' + monenaId));
                                    rowMon.remove().draw();

                                    Swal.fire(
                                          '¡Borrado!',
                                          'Tu registro ha sido borrado.',
                                          'success'
                                    );
                              }
                              else {
                                    Swal.fire(
                                          '¡Opss!',
                                          'Al parecer hubo un detalle en el eliminado.Vuelve a intentar más tarde o comunícate con el depto. de TI',
                                          'warning'
                                    );
                              }
                        },
                        error: function (response) {
                              Swal.fire(
                                    '¡Opss!',
                                    'Al parecer hubo un detalle en el eliminado.Vuelve a intentar más tarde o comunícate con el depto. de TI.',
                                    'warning'
                              );
                        }
                  });
            }
      });
}

BuscarMoneda = (monedaId, url) => {
      $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            data: { id: monedaId },
            success: function (response) {
                  if (response.Code == 0 && response.HttpStatusCode == 200) {
                        $('#idMoneda').val(response.Result.MonedaId);
                        $('#moneda').val(response.Result.Moneda);
                        $('#abreviatura').val(response.Result.MonedaAbreviatura);
                  }
                  else {
                        Swal.fire(
                              '¡Opss!',
                              'Al parecer hubo un detalle al mostrar los datos.Vuelve a intentar más tarde o comunícate con el depto. de TI',
                              'warning'
                        );
                  }
            },
            error: function (response) {
                  Swal.fire(
                        '¡Opss!',
                        'Al parecer hubo un detalle al mostrar los datos.Vuelve a intentar más tarde o comunícate con el depto. de TI',
                        'warning'
                  );
            }
      });
}

LimpiarFormMoneda = () => {
      $('#formMoneda')[0].reset();
      $('#idMoneda').val('');
}