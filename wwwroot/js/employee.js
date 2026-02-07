let rowDeleteId = null;                 // Stores single row delete Id
let isBulkDelete = false;               // Flag to differentiate delete type

document.addEventListener("DOMContentLoaded", function () { // Run after DOM is loaded

    const selectAll = document.getElementById("selectAll"); // Select-all checkbox
    const rowCheckboxes = document.querySelectorAll(".row-checkbox"); // Row checkboxes

    if (!selectAll) return;              // Exit if select-all checkbox not present

    // Select / Deselect all
    selectAll.addEventListener("change", function () {
        rowCheckboxes.forEach(cb => cb.checked = selectAll.checked); // Toggle all checkboxes
    });

    // Update "Select All" when individual checkboxes change
    rowCheckboxes.forEach(cb => {
        cb.addEventListener("change", function () {
            selectAll.checked =
                Array.from(rowCheckboxes).every(x => x.checked); // Sync select-all state
        });
    });
});

// SINGLE row delete
function openDeleteModal(id) {
    rowDeleteId = id;                   // Store selected row Id
    isBulkDelete = false;               // Mark as single delete
    new bootstrap.Modal(document.getElementById('deleteModal')).show(); // Show delete modal
}

// BULK delete
function openBulkDeleteModal() {
    const checked = document.querySelectorAll("input[name='ids']:checked"); // Selected rows

    if (checked.length === 0) {
        alert("Please select at least one record to delete."); // No selection alert
        return;
    }

    isBulkDelete = true;                // Mark as bulk delete
    new bootstrap.Modal(document.getElementById('deleteModal')).show(); // Show delete modal
}

// Confirm delete (called from modal Delete button)
function confirmDelete() {
    if (isBulkDelete) {
        // bulk delete
        document.getElementById("deleteForm").submit(); // Submit bulk delete form
    } else {
        // single delete
        const form = document.createElement("form");   // Create dynamic form
        form.method = "post";                           // Set POST method
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

// Load Edit Modal content dynamically
function loadEdit(id) {
    fetch(`/Employee/Edit/${id}`)       // Fetch edit partial view
        .then(response => response.text())
        .then(html => {
            document.getElementById("editContent").innerHTML = html; // Inject modal content

            var modal = new bootstrap.Modal(
                document.getElementById("editModal")
            );
            modal.show();               // Show edit modal
        })
        .catch(err => console.error(err)); // Log fetch errors
}
