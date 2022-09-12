var tablaProveedores;
var tablaMoneda;
var tablaRecibo;
$(document).ready(function () {
	tablaProveedores = $('#tablaProveedor').DataTable({ "language": { "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json" }, "destroy": true, });

	tablaMoneda = $('#tablaMoneda').DataTable({ "language": { "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json" }, "destroy": true, });

	tablaRecibo = $('#tablaRecibo').DataTable({ "language": { "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json" }, "destroy": true, });
});
