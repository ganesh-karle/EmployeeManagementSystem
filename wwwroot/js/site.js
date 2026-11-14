// Theme and Sidebar Initialization
document.addEventListener("DOMContentLoaded", function () {
    initTheme();
    initSidebar();

    // Auto-hide alert banners after 4 seconds
    setTimeout(function () {
        $(".alert").fadeOut("slow");
    }, 4000);
});

// Theme Management
function initTheme() {
    const savedTheme = localStorage.getItem("ems_theme") || "light";
    applyTheme(savedTheme);
}

function applyTheme(theme) {
    document.documentElement.setAttribute("data-theme", theme);
    document.documentElement.setAttribute("data-bs-theme", theme);
    localStorage.setItem("ems_theme", theme);

    const themeIcon = document.getElementById("themeIcon");
    const themeLabel = document.getElementById("themeLabel");
    const lightRadio = document.getElementById("themeRadioLight");
    const darkRadio = document.getElementById("themeRadioDark");

    if (theme === "dark") {
        if (themeIcon) themeIcon.className = "far fa-sun text-warning";
        if (themeLabel) themeLabel.textContent = "Light mode";
        if (darkRadio) darkRadio.checked = true;
    } else {
        if (themeIcon) themeIcon.className = "far fa-moon";
        if (themeLabel) themeLabel.textContent = "Dark mode";
        if (lightRadio) lightRadio.checked = true;
    }

    // Trigger custom event so charts can re-render if needed
    window.dispatchEvent(new CustomEvent('themeChanged', { detail: { theme: theme } }));
}

function toggleTheme() {
    const currentTheme = document.documentElement.getAttribute("data-theme") || "light";
    const newTheme = currentTheme === "dark" ? "light" : "dark";
    applyTheme(newTheme);
}

// Sidebar Slide / Collapse Toggle (< and > toggle)
function initSidebar() {
    const isCollapsed = localStorage.getItem("ems_sidebar_collapsed") === "true";
    if (isCollapsed) {
        applySidebarCollapsed(true);
    }
}

function applySidebarCollapsed(collapsed) {
    const sidebar = document.querySelector(".sidebar");
    const toggleIcon = document.querySelector("#sidebarCollapseBtn i");
    if (!sidebar) return;

    if (collapsed) {
        sidebar.classList.add("collapsed");
        if (toggleIcon) toggleIcon.className = "fas fa-chevron-right";
        localStorage.setItem("ems_sidebar_collapsed", "true");
    } else {
        sidebar.classList.remove("collapsed");
        if (toggleIcon) toggleIcon.className = "fas fa-chevron-left";
        localStorage.setItem("ems_sidebar_collapsed", "false");
    }
}

function toggleSidebar() {
    const sidebar = document.querySelector(".sidebar");
    if (!sidebar) return;
    const isCurrentlyCollapsed = sidebar.classList.contains("collapsed");
    applySidebarCollapsed(!isCurrentlyCollapsed);
}

// Settings Modal
function saveSettingsModal() {
    const isDark = document.getElementById("themeRadioDark")?.checked;
    applyTheme(isDark ? "dark" : "light");

    // Close modal
    const modalEl = document.getElementById("settingsModal");
    const modalInstance = bootstrap.Modal.getInstance(modalEl);
    if (modalInstance) {
        modalInstance.hide();
    }
}

// Global Quick Monthly Attendance & Leave Modal
window.showAttendanceModal = function(employeeId) {
    const modalEl = document.getElementById('globalAttendancePreviewModal');
    if (!modalEl) return;

    const modal = new bootstrap.Modal(modalEl);
    modal.show();

    const tbody = document.getElementById('globalModalRecordsBody');
    if (tbody) {
        tbody.innerHTML = '<tr><td colspan="6" class="text-center py-4 text-muted"><i class="fas fa-spinner fa-spin me-2"></i> Loading attendance details...</td></tr>';
    }

    fetch(`/Employee/GetMonthlyAttendanceJson?id=${employeeId}`)
        .then(response => {
            if (!response.ok) throw new Error('Network error');
            return response.json();
        })
        .then(data => {
            const nameEl = document.getElementById('globalModalEmployeeName');
            const subEl = document.getElementById('globalModalEmployeeSub');
            const headerEl = document.getElementById('globalModalMonthHeader');
            const presEl = document.getElementById('globalModalDaysPresent');
            const leaveEl = document.getElementById('globalModalDaysLeave');
            const lateEl = document.getElementById('globalModalDaysLate');
            const balEl = document.getElementById('globalModalLeaveBalance');
            const linkEl = document.getElementById('globalModalFullViewLink');

            if (nameEl) nameEl.textContent = data.fullName;
            if (subEl) subEl.textContent = `${data.designation} • ${data.department} • ${data.monthName}`;
            if (headerEl) headerEl.textContent = data.monthName;
            if (presEl) presEl.textContent = data.daysPresent;
            if (leaveEl) leaveEl.textContent = data.daysOnLeave;
            if (lateEl) lateEl.textContent = data.daysLate;
            if (balEl) balEl.textContent = data.availableLeaves;
            if (linkEl) linkEl.href = `/Employee/MonthlyAttendance/${employeeId}`;

            let rowsHtml = '';
            if (data.records && data.records.length > 0) {
                data.records.forEach(r => {
                    let badgeClass = 'bg-success-subtle text-success border border-success-subtle';
                    let icon = 'fa-check-circle';
                    if (r.status === 'Late') {
                        badgeClass = 'bg-warning-subtle text-warning-emphasis border border-warning-subtle';
                        icon = 'fa-clock';
                    } else if (r.status === 'On Leave') {
                        badgeClass = 'bg-info-subtle text-info border border-info-subtle';
                        icon = 'fa-umbrella-beach';
                    } else if (r.status === 'Absent') {
                        badgeClass = 'bg-danger-subtle text-danger border border-danger-subtle';
                        icon = 'fa-times-circle';
                    }

                    rowsHtml += `
                        <tr>
                            <td class="fw-bold">${r.date}</td>
                            <td><span class="badge bg-light text-dark border">${r.day}</span></td>
                            <td>${r.clockIn}</td>
                            <td>${r.clockOut}</td>
                            <td><span class="badge ${badgeClass} px-2 py-1"><i class="fas ${icon} me-1"></i> ${r.status}</span></td>
                            <td><small class="text-muted">${r.remarks || '-'}</small></td>
                        </tr>
                    `;
                });
            } else {
                rowsHtml = '<tr><td colspan="6" class="text-center py-3 text-muted">No attendance logs found for this month.</td></tr>';
            }
            if (tbody) tbody.innerHTML = rowsHtml;
        })
        .catch(err => {
            if (tbody) tbody.innerHTML = '<tr><td colspan="6" class="text-center py-3 text-danger"><i class="fas fa-exclamation-triangle me-1"></i> Failed to load attendance records.</td></tr>';
        });
};
