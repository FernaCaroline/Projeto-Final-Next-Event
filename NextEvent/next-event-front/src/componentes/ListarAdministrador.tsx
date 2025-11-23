import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Adiministrador from "../interfaces/Adiministrador";
import axios from "axios";

function ListarAdministrador() {

    const [admins, setAdmins] = useState<Adiministrador[]>([]);

    useEffect(() => {
        listarAdminsAPI();
    }, []);

    async function listarAdminsAPI() {
        try {
            const resposta = await axios.get<Adiministrador[]>(
                "http://localhost:5162/api/administrador/listar"
            );
            setAdmins(resposta.data);
        } catch (error) {
            console.log("Erro ao listar ADM:", error);
        }
    }

    async function deletarAdministrador(id: string) {
        try {
            await axios.delete(
                `http://localhost:5162/api/administrador/deletar/${id}`
            );
            listarAdminsAPI();
        } catch (error) {
            console.log("Erro ao deletar ADM:", error);
        }
    }

    return (
        <div>
            <h1>Listar Administradores</h1>
            <table>
                <thead>
                <tr>
                    <th>ID</th>
                    <th>Nome</th>
                    <th>Email</th>
                    <th>Criado em</th>
                    <th>Editar</th>
                    <th>Excluir</th>
                </tr>
                </thead>

                <tbody>
                {admins.map(a => (
                    <tr key={a.id}>
                        <td>{a.id}</td>
                        <td>{a.nome}</td>
                        <td>{a.email}</td>
                        <td>{a.criadoEm}</td>

                        <td>
                            <Link to={`/administrador/atualizar/${a.id}`}>
                                Editar
                            </Link>
                        </td>

                        <td>
                            <button onClick={() => deletarAdministrador(a.id!)}>
                                Deletar
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>

            </table>
        </div>
    );
}

export default ListarAdministrador;
