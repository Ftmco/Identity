

export default function pipeline(context: any, middleware: any, index: number) {
    const nextMiddleware = middleware[index]
    if (!nextMiddleware) {
        return context.next;
    }

    return () => {
        const nexPipeline = pipeline(context, middleware, index + 1);
        nextMiddleware({
            ...context,
            next: nexPipeline
        })
    }
}