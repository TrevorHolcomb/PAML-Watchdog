function filtersFromParameterList(parameterList) {
    return parameterList.map(function (parameter) {
        return filterFromParameter(parameter);
    });
}

function filterFromParameter(parameter) {
    if (parameter.Type.toLowerCase() === "exception") {
        return exceptionFilterFromParameter(parameter);
    } else if (parameter.Type.toLowerCase() === "string") {
        return stringFilterFromParameter(parameter);
    } else if (parameter.Type.toLowerCase() === "integer") {
        return integerFilterFromParameter(parameter);
    } else {
        throw "Parameter Type Not Matched: " + parameter.Type;
    }
}

function exceptionFilterFromParameter(parameter) {
    return {
        "type": "string",
        "id": parameter.Name
    };
}

function stringFilterFromParameter(parameter) {
    return {
        "type": "string",
        "id":parameter.Name
    }
}

function integerFilterFromParameter(parameter) {
    return {
        "type": "integer",
        "id": parameter.Name
    }
}