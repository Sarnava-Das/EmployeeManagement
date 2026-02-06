let rowDeleteId = null;
let isBulkDelete = false;


document.addEventListener("DOMContentLoaded", function () {

    const selectAll = document.getElementById("selectAll");
    const rowCheckboxes = document.querySelectorAll(".row-checkbox");

    if (!selectAll) return;

    // Select / Deselect all
    selectAll.addEventListener("change", function () {
        rowCheckboxes.forEach(cb => cb.checked = selectAll.checked);
    });

    // Update "Select All" when individual checkboxes change
    rowCheckboxes.forEach(cb => {
        cb.addEventListener("change", function () {
            selectAll.checked =
                Array.from(rowCheckboxes).every(x => x.checked);
        });
    });
});

// SINGLE row delete
function openDeleteModal(id) {
    rowDeleteId = id;
    isBulkDelete = false;
    new bootstrap.Modal(document.getElementById('deleteModal')).show();
}

// BULK delete
function openBulkDeleteModal() {
    const checked = document.querySelectorAll("input[name='ids']:checked");

    if (checked.length === 0) {
        alert("Please select at least one record to delete.");
        return;
    }

    isBulkDelete = true;
    new bootstrap.Modal(document.getElementById('deleteModal')).show();
}

// Confirm delete (called from modal Delete button)
function confirmDelete() {
    if (isBulkDelete) {
        // bulk delete
        document.getElementById("deleteForm").submit();
    } else {
        // single delete
        const form = document.createElement("form");
        form.method = "post";
        form.action = "/Employee/Delete";

        const input = document.createElement("input");
        input.type = "hidden";
        input.name = "id";
        input.value = rowDeleteId;

        form.appendChild(input);
        document.body.appendChild(form);
        form.submit();
    }
}
// Load Edit Modal content dynamically
function loadEdit(id) {
    fetch(`/Employee/Edit/${id}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById("editContent").innerHTML = html;

            var modal = new bootstrap.Modal(
                document.getElementById("editModal")
            );
            modal.show();
        })
        .catch(err => console.error(err));
}


