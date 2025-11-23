import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import Adiministrador from "../interfaces/Adiministrador";

function AtualizarAdministrador() {

    const { id } = useParams();
    const navigate = useNavigate();

    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    useEffect(() => {
        buscarAdministradorAPI();
    }, []);

    async function buscarAdministradorAPI() {
        try {
            const resposta = await axios.get<Adiministrador>(
                `http://localhost:5162/api/administrador/buscar/${id}`
            );

            setNome(resposta.data.nome);
            setEmail(resposta.data.email);
            // senha não preenche porque está hash (igual ao participante)

        } catch (error) {
            console.log("Erro ao buscar ADM:", error);
        }
    }

    async function atualizarAdministradorAPI() {
        try {
            const adminAtualizado = { nome, email, senha };

            await axios.put(
                `http://localhost:5162/api/administrador/atualizar/${id}`,
                adminAtualizado
            );

            navigate("/administrador/listar");

        } catch (error) {
            console.log("Erro ao atualizar ADM:", error);
        }
    }

    function submit(e: any) {
        e.preventDefault();
        atualizarAdministradorAPI();
    }

    return (
        <div>
            <h1>Atualizar Administrador</h1>

            <form onSubmit={submit}>
                <div>
                    <label>Nome:</label>
                    <input value={nome} onChange={(e) => setNome(e.target.value)} />
                </div>

                <div>
                    <label>Email:</label>
                    <input value={email} onChange={(e) => setEmail(e.target.value)} />
                </div>

                <div>
                    <label>Nova Senha:</label>
                    <input value={senha} onChange={(e) => setSenha(e.target.value)} />
                </div>

                <button type="submit">Salvar</button>
            </form>
        </div>
    );
}

export default AtualizarAdministrador;
