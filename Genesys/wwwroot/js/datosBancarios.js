let datatable; //creamos una variable con el comando let datatable

//cargamos una funcion para saber en que momento esta cargando nuestra vista
$(document).ready(function () {
    loadDataTable(); //Llamamos a la funcion loadDataTable
});
//Creamos la funcion loadDataTable
function loadDataTable() {
    datatable = $('#tblDatos').DataTable({ //capturamos el id de nuestra tabla de la vista razor por medio de jquery
        //#tblDatos es el id de nuestra tabla en index.html
        "language": { //cambia el contenido de la tabla a espa;ol 
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar page _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": { //ajax necesita la url del metodo ObtenerTodos
            "url": "/Admin/DatosBancarios/ObtenerTodos"
        },
        "columns": [ // en la seccion columnas se renderizan las columnas o propiedades de la tabla
            { "data": "idDatosBancarios" },
            { "data": "nombreBanco" },
            { "data": "numeroCuenta" },
            { "data": "clabeInterbancaria" },
            { "data": "prestamos" },
            {
                "render": function (data, type, row, meta) {
                    return row.empleado.nombres+ " " + row.empleado.apPaterno + " "+ row.empleado.apMaterno;
                }
            },
            {
                "data": "statusDatosBancarios", //como estado es booleana creamos una funcion para imprimir activo (true) o inactivo(false)
                "render": function (data) {//para eso usamos la funcion render y armamos la funcion, mandando data como parametro
                    if (data == true) {
                        return "Activo";
                    }
                    else {
                        return "Inactivo";
                    }
                },
            },
            {
                "data": "idDatosBancarios", //la columna vacia tiene que ver con los botones, le pasamos el id de data mediante un render
                "render": function (data) {//data  trae el id del modelo... para aparecer las comillas invertidas usamos alt96
                    //esto es para renderizar codigo html
                    return ` 
                        <div class="text-center">
                            <a href="/Admin/DatosBancarios/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer"> 
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/Admin/DatosBancarios/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash3-fill"></i>
                            </a>
                        </div>
                    `;
                },
            }
        ]
    });
}
function Delete(url) {
    swal({
        title: "¿Está seguro de eliminar el dato bancario?",
        text: "Este registro no se podrá recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}