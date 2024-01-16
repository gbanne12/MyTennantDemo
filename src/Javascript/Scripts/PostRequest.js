// Placeholder for the CRM environment base url
const crmUrl = '##CRM_URL##';

// Placeholder for entityName
const entityName = '##ENTITY_NAME##';

// Placeholder for json data to be included in the request
const jsonBody = {"##KEY": "VALUE##"};


// Modify the URL with the dynamic entity name
const apiUrl = crmUrl + `${entityName}`;


const response = await fetch(
    apiUrl,
    {
        method: "POST",
        headers: {
            "OData-MaxVersion": "4.0",
            "OData-Version": "4.0",
            "Content-Type": "application/json; charset=utf-8",
            "Accept": "application/json",
            "Prefer": "odata.include-annotations=*"
        },
        body: JSON.stringify(jsonBody)
    }
);

    if (response.ok) {
        const uri = response.headers.get("OData-EntityId");
        const regExp = /\(([^)]+)\)/;
        const matches = regExp.exec(uri);
        const newId = matches[1];
        console.log(newId);
        return newId;
    } else {
        const json = await response.json();
        return json.error.message;
    }
