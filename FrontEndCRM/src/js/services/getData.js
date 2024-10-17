const getData = async (url) => {
    let result = [];

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
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

const GetData = {
    Get: getData
};

export default GetData;