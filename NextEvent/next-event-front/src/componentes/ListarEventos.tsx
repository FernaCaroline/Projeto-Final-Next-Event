import { useEffect, useState } from "react";
import Evento from "../interfaces/Evento";
import axios from "axios";


function ListarEventos() {
    return (
    <div>
      <h1>Listar Eventos</h1>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Descrição</th>
            <th>Início das inscrições</th>
            <th>Fim das inscrições</th>
            {/* Aqui coloquei os dados que acho relevantes para o usuário final, adptando os nome para que façam sentido com o contexto */}
          </tr>
        </thead>
      </table>
    </div>
  );
}

export default ListarEventos;