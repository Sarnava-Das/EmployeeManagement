let rowDeleteId = null;                
let isBulkDelete = false;              

document.addEventListener("DOMContentLoaded", function () { // Run after DOM is loaded

    const selectAll = document.getElementById("selectAll"); 
    const rowCheckboxes = document.querySelectorAll(".row-checkbox"); 

    if (!selectAll) return; // Exit if select-all checkbox not present

    
    selectAll.addEventListener("change", function () {
        rowCheckboxes.forEach(cb => cb.checked = selectAll.checked); // Toggle all checkboxes i.e select/deselect all
    });

    // Update "Select All" when individual checkboxes change
    rowCheckboxes.forEach(cb => {
        cb.addEventListener("change", function () {
            selectAll.checked =
                Array.from(rowCheckboxes).every(x => x.checked); // Sync select-all state
        });
    });
});

// single row delete to show delete warning modal
function openDeleteModal(id) {
    rowDeleteId = id;                  
    isBulkDelete = false;               
    new bootstrap.Modal(document.getElementById('deleteModal')).show(); 
}

// bulk delete 
function openBulkDeleteModal() {
    const checked = document.querySelectorAll("input[name='ids']:checked"); // Selected rows

    if (checked.length === 0) {
        alert("Please select at least one record to delete."); // No selection browser alert
        return;
    }

    isBulkDelete = true;                
    new bootstrap.Modal(document.getElementById('deleteModal')).show(); // Show delete warning modal
}

// confirmDelete() called from delete warning modal by clicking Delete button
function confirmDelete() {
    if (isBulkDelete) {
          document.getElementById("deleteForm").submit(); // Submit bulk delete form
    } else {
        // single delete
        const form = document.createElement("form");   // Create dynamic form
        form.method = "post";                           
        form.action = "/Employee/Delete";               // Target delete action

        const input = document.createElement("input"); // Hidden input for Id
        input.type = "hidden";
        input.name = "id";
        input.value = rowDeleteId;

        form.appendChild(input);                        // Append Id input
        document.body.appendChild(form);                // Append form to DOM
        form.submit();                                  // Submit delete request
    }
}

// Load edit modal content dynamically i.e server-side rendering of the partial view edit modal since using fetch
function loadEdit(id) {
    fetch(`/Employee/Edit/${id}`)       // fetch edit partial view
        .then(response => response.text())
        .then(html => {
            document.getElementById("editContent").innerHTML = html; // Inject modal content

            var modal = new bootstrap.Modal(
                document.getElementById("editModal")
            );
            modal.show();               
        })
        .catch(err => console.error(err)); // Log fetch errors
}
