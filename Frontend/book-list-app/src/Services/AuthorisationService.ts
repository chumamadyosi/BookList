import axios from "axios";
import { LoginPayload } from "../Interfaces/LoginPayload";
import { LoginResponse } from "../Interfaces/LoginResponse";

const API_URL = process.env.REACT_APP_Book_List_API + "Authentication";

export const AuthenticateUser = async (payload: LoginPayload): Promise<LoginResponse> => {
  const response = await axios.post<LoginResponse>(`${API_URL}/login`, payload);
  return response.data;
};
