// noinspection JSUnusedGlobalSymbols

function initializeDialog(dialog, reference) {
    // noinspection JSUnusedLocalSymbols
    dialog.addEventListener("close", async event => {
        // noinspection JSUnresolvedFunction
        await reference.invokeMethodAsync("OnCloseEvent", dialog.returnValue);
    });
}

function showDialog(dialog) {
    if (!dialog.open) {
        dialog.show();
    }
}

function showDialogModal(dialog) {
    if (!dialog.open) {
        dialog.showModal();
    }
}

function closeDialog(dialog) {
    if (dialog.open) {
        dialog.close();
    }
}
