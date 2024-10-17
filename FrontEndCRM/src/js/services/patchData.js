const patchData = async (url, body) => {
    let result = [];

    try {
        const response = await fetch(url, {
            method: 'PATCH', 
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body) 
        });

        if (!response.ok) {
            throw new Error('Se ha registrado un error en la solicitud.');
        }

        result = await response.json();
        return result;
    }
    catch (error) {
        console.error('Error:', error);
    }
};

const PatchData = {
    Patch: patchData
};

export default PatchData;
