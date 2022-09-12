var nuevoProveedor;

OpenModalProveedor = (nuevo, proveedorId) => {
	$('#modalProveedor').modal('show');
	var url = $('.OpenModalProveedorClass').data('request-url');
	nuevoProveedor = nuevo;
	LimpiarFormProveedor();

	if (nuevo) {
            $('#ProveedorModalLabel').text('Nuevo Proveedor'); 
	}
	else {
            $('#ProveedorModalLabel').text('Editar Proveedor'); 
		BuscarProveedor(proveedorId, url);
	}
}

GuardarProveedor = () => {
	var url = nuevoProveedor ? $('.nuevoElemento').data('request-url') : $('.editarElemento').data('request-url');
	var formData = new FormData();

      formData.append('ProveedorId', $('#idProveedor').val());
	formData.append('NombreCortoProveedor', $('#nombreCorto').val());
	formData.append('NombreLargoProveedor', $('#nombreLargo').val());
	formData.append('CorreoProveedor', $('#correo').val());
	formData.append('TelefonoProveedor', $('#telefono').val());
	formData.append('DireccionProveedor', $('#direccion').val());

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
                              $('#modalProveedor').modal('hide');
                              if (response.Code == 0 && response.HttpStatusCode == 200) {
                                    Swal.fire(
                                          '¡Guardado!',
                                          'Tu registro ha sido guardado correctamente.',
                                          'success'
                                    );
                                    if (nuevoProveedor && response.Result != null) {
                                          var icono = '<i class="fas fa-users"></i>';
                                          var opciones = `
                                                <a href="#" class="OpenModalProveedorClass" title="Editar" data-request-url=" `+ urlProv + `" onclick="OpenModalProveedor(false,  ` + response.Result.ProveedorId + `)"><i class="far fa-edit"></i></a>
                                                &nbsp
                                                <a href="#" class="EliminarProveedorClass" title="Eliminar" data-request-url=" `+ urlDelProv + `" onclick="EliminarProveedor(` + response.Result.ProveedorId + `)"><i class="far fa-trash-alt"></i></a>
                                    `;
                                          var rowNode = tablaProveedores.row.add([icono, response.Result.NombreCortoProveedor, response.Result.CorreoProveedor, response.Result.TelefonoProveedor, response.Result.DireccionProveedor, opciones]).draw().node();
                                          $(rowNode).attr('id', response.Result.ProveedorId);
                                    }
                                    else if (!nuevoProveedor && response.Result != null) {
                                          var columnas = $('#tablaProveedor tbody tr#' + response.Result.ProveedorId + '').find('td');
                                          var icono = `<i class="fas fa-users"></i>`;
                                          columnas.eq(0).html(icono);
                                          columnas.eq(1).text(response.Result.NombreCortoProveedor);
                                          columnas.eq(2).text(response.Result.CorreoProveedor);
                                          columnas.eq(3).text(response.Result.TelefonoProveedor);
                                          columnas.eq(4).text(response.Result.DireccionProveedor);
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

EliminarProveedor = (proveedorId) => {
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
                  var url = $('.EliminarProveedorClass').data('request-url');
                  $.ajax({
                        url: url,
                        type: 'POST',
                        dataType: 'json',
                        data: { id: proveedorId },
                        success: function (response) {
                              if (response.Code == 0 && response.HttpStatusCode == 200) {
                                    var rowProv = tablaProveedores.row($('#' + proveedorId));
                                    rowProv.remove().draw();

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

BuscarProveedor = (proveedorId, url) => {
      $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            data: { id: proveedorId },
            success: function (response) {
                  if (response.Code == 0 && response.HttpStatusCode == 200) {
                        $('#idProveedor').val(response.Result.ProveedorId);
                        $('#nombreCorto').val(response.Result.NombreCortoProveedor);
                        $('#nombreLargo').val(response.Result.NombreLargoProveedor);
                        $('#correo').val(response.Result.CorreoProveedor);
                        $('#telefono').val(response.Result.TelefonoProveedor);
                        $('#direccion').val(response.Result.DireccionProveedor);
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

LimpiarFormProveedor = () => {
      $('#formProveedor')[0].reset();
      $('#idProveedor').val('');
}