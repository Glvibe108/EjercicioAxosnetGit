OpenModalRecibos = () => {
	$('#modalRecibo').modal('show');
	$('#ReciboModalLabel').text('Nuevo Recibo');
	LimpiarFormRecibos();
	ListarMonedas();
	ListaProveedores();

}

ListarMonedas = () => {
	$.ajax({
		url: urlMonRec,
		type: 'GET',
		dataType: 'json',
		success: function (response) {
			$('#monedaRecibo').append('<option value="">Seleccione una moneda</option>');
			$.each(response, function (index, item) {
				$('#monedaRecibo').append('<option value="' + item.Value + '">' + item.Text + '</option>');
			});
		},
		error: function (response) {
			console.log(response)
		}
	})
}
ListaProveedores = () => {
	$.ajax({
		url: urlProvRec,
		type: 'GET',
		dataType: 'json',
		success: function (response) {
			$('#proveedorRecibo').append('<option value="">Seleccione un proveedor</option>');
			$.each(response, function (index, item) {
				$('#proveedorRecibo').append('<option value="' + item.Value + '">' + item.Text + '</option>');
			});
		},
		error: function (response) {
			console.log(response)
		}
	})
}
GuardarRecibo = () => {
	var url = $('.OpenModalReciboClass').data('request-url');
	var formData = new FormData();

	formData.append('FechaReciboAdd', $('#fechaRecibo').val());
	formData.append('MontoRecibo', $('#monto').val());
	formData.append('MonedaIdAdd', $('#monedaRecibo').val());
	formData.append('ProveedorIdAdd', $('#proveedorRecibo').val());
	formData.append('Comentario', $('#comentario').val());

	Swal.fire({
		title: '¿Estas seguro de guardar el registro?',
		text: 'Una vez guardado se cierra el recibo y no podrá ser editado o eliminado (haciendo mímica a una autorización)',
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
					$('#modalRecibo').modal('hide');
					if (response.Code == 0 && response.HttpStatusCode == 200) {
						Swal.fire(
							'¡Guardado!',
							'Tu registro ha sido guardado correctamente.',
							'success'
						);
						if (response.Result != null) {
							var icono = '<i class="fas fa-file-signature"></i>';
							var opciones = `
                                                <a href="#" class="OpenModalVerReciboClass" title="Ver recibo" data-request-url=" `+ urlRec + `" onclick="OpenModalVerRecibo(` + response.Result.ReciboId + `)"><i class="fas fa-eye"></i></a>`;

							var rowNode = tablaRecibo.row.add([icono, response.Result.FolioRecibo, response.Result.MontoRecibo, response.Result.NombreMoneda, response.Result.NombreProveedor, response.Result.Comentario, opciones]).draw().node();
							$(rowNode).attr('id', response.Result.ReciboId);
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

OpenModalVerRecibo = (reciboId) => {
	var url = $('.OpenModalVerReciboClass').data('request-url');
	$.ajax({
		url: url,
		type: 'GET',
		dataType: 'json',
		data: { id: reciboId },
		success: function (response) {
			$('#modalVerRecibo').modal('show');
			if (response.Code == 0 && response.HttpStatusCode == 200) {
				$('#VerReciboModalLabel').text(response.Result.FolioRecibo);
				$('#fechaVerFolio').text(response.Result.FechaCortaRecibo);
				$('#montoVerRecibo').text(response.Result.MontoRecibo);
				$('#proveedorVerRecibo').text(response.Result.NombreProveedor);
				$('#monedaVerRecibo').text(response.Result.NombreMoneda);
				$('#comentarioVerRecibo').text(response.Result.Comentario);
				
			}
			
		}

	});
}

LimpiarFormRecibos = () => {
	$('#formRecibo')[0].reset();
}