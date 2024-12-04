import { useState, useCallback } from 'react';

type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE';

interface FetchOptions {
    url: string;
    method?: HttpMethod;
    body?: any;
    headers?: Record<string, string>;
    successMessage?: string;
    errorMessage?: string;

}

function useApi<T>() {
    const [data, setData] = useState<T | null>(null);
    const [loading, setLoading] = useState<boolean>(false);


    const handleFetch = useCallback(async (options: FetchOptions) => {
        const { url, method = 'GET', body, headers = {} } = options;
        console.log("Fetching ", url)
        setLoading(true);

        try {
            const response = await fetch(url, {
                method,
                headers: {
                    'Content-Type': 'application/json',
                    ...headers,
                },
                body: body ? body : undefined,
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const result = await response.json();
            console.log(result);
            if (!result)
                window.addErrorMessage("Something went wrong", "Error");

            if (result.IsSuccessfull) {
                setData(result.Data as T);

                if (options.successMessage)
                    window.addInfoMessage(options.successMessage);
            }
        } catch (e) {
            if (options.errorMessage)
                window.addErrorMessage(`Error while fetching ${url}: ${e instanceof Error ? e.message : options.errorMessage}`, 'Error');
            else
                window.addErrorMessage(`Error while fetching ${url}: ${e instanceof Error ? e.message : 'An unknown error occurred'}`, 'Error');

            console.log(e instanceof Error ? e : new Error('An unknown error occurred'));
        } finally {
            setLoading(false);
        }
    }, []);

    return { data, loading, handleFetch };
}

export default useApi;