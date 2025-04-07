import axios from 'axios';
import { Author } from '../Interfaces/uthorisationInterfaces/Author';

const API_URL = process.env.REACT_APP_Book_List_API + 'author'; 

class AuthorService {

  static getToken(): string | null {
    return localStorage.getItem('token');
  }

  static getRequestHeaders() {
    const token = this.getToken();
    return token ? { Authorization: `Bearer ${token}` } : {};
  }

  static async getAuthors(): Promise<Author[]> {
    try{
        const response = await axios.get(API_URL,{
            headers: this.getRequestHeaders(),
        });
        return response.data;
    }catch(error)
    {
      throw new Error('Error fetching books');
    }
  }
}

export default AuthorService;
