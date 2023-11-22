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
            "url": "/Admin/Usuario/ObtenerTodos"
        },
        "columns": [ // en la seccion columnas se renderizan las columnas o propiedades de la tabla
            { "data": "email" },
            { "data": "nombres" },
            { "data": "apellidos" },
            { "data": "role" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    let hoy = new Date().getTime();
                    let bloqueo = new Date(data.lockoutEnd).getTime();
                    if (bloqueo > hoy) {
                        // Usuario esta Bloqueado
                        return `
                            <div class="text-center">
                               <a onclick=BloquearDesbloquear('${data.id}') class="btn btn-danger text-white" style="cursor:pointer", width:150px >
                                    <i class="bi bi-unlock-fill"></i> Desbloquear
                               </a> 
                            </div>
                        `;
                    }
                    else {
                        return `
                            <div class="text-center">
                               <a onclick=BloquearDesbloquear('${data.id}') class="btn btn-purple text-white" style="cursor:pointer", width:150px >
                                    <i class="bi bi-lock-fill"></i> Bloquear
                               </a> 
                            </div>
                        `;
                    }

                }
            }
        ]
    });
}

function BloquearDesbloquear(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/Usuario/BloquearDesbloquear',
        data: JSON.stringify(id),
        contentType: "application/json",
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