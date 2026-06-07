(function () {
    const agendaList = document.getElementById('agendaList');
    const addAgendaBtn = document.getElementById('addAgendaBtn');
    const template = document.getElementById('agendaItemTemplate');

    if (!agendaList || !addAgendaBtn || !template) {
        return;
    }

    function getAgendaItems() {
        return agendaList.querySelectorAll('.agenda-item');
    }

    function reindexAgendaItems() {
        const items = getAgendaItems();

        items.forEach((item, index) => {
            item.dataset.index = index;
            item.querySelector('.agenda-item-number').textContent = `بند ${index + 1}`;

            const topicInput = item.querySelector('input[name*="Topic"]');
            const durationInput = item.querySelector('input[name*="Duration"]');

            if (topicInput) {
                topicInput.name = `Input.AgendaItems[${index}].Topic`;
                topicInput.id = `Input_AgendaItems_${index}__Topic`;
            }

            if (durationInput) {
                durationInput.name = `Input.AgendaItems[${index}].Duration`;
                durationInput.id = `Input_AgendaItems_${index}__Duration`;
            }
        });

        const removeButtons = agendaList.querySelectorAll('.agenda-remove-btn');
        removeButtons.forEach((button) => {
            button.style.display = items.length > 1 ? '' : 'none';
        });
    }

    function addAgendaItem() {
        const index = getAgendaItems().length;
        const html = template.innerHTML
            .replace(/__index__/g, index)
            .replace(/__number__/g, index + 1);

        agendaList.insertAdjacentHTML('beforeend', html);
        reindexAgendaItems();
    }

    agendaList.addEventListener('click', function (event) {
        const removeBtn = event.target.closest('.agenda-remove-btn');
        if (!removeBtn) {
            return;
        }

        const items = getAgendaItems();
        if (items.length <= 1) {
            return;
        }

        removeBtn.closest('.agenda-item').remove();
        reindexAgendaItems();
    });

    addAgendaBtn.addEventListener('click', addAgendaItem);
    reindexAgendaItems();
})();
