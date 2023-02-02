export default function Page({ params }: { params: { code: string } }) {
    return (
        <h1>{params.code}</h1>
   )
}